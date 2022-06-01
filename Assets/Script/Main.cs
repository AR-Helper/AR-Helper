using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Main
{
    public static Main INSTANCE = new Main();

    public static bool isViewMode = false;

    public static byte[] FormatSerializable(object serializable)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            formatter.Serialize(memoryStream, serializable);
            return memoryStream.ToArray();
        }
    }

    public static Type DeserializeFormatted<Type>(byte[] formatRes)
        where Type : class
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream(formatRes))
        {
            return formatter.Deserialize(memoryStream) as Type;
        }
    }

    private static string GetPersitentLocation(string filename)
    {
        return Path.GetFullPath(Path.Combine(Application.persistentDataPath, filename));
    }

    // public FileStream GetPersistentFileStream(string filename)
    // {
    //     throw new System.NotImplementedException();
    // }
    public static byte[] ReadPersistentSimpleFile(string filename)
    {
        string persistentFileName = GetPersitentLocation(filename); // Path.GetFullPath(Path.Combine(Application.persistentDataPath, filename));
        // Debug.Log("ReadPersistentSimpleFile: " + persistentFileName);
        
        return File.ReadAllBytes(persistentFileName);
    }

    public static void SavePersistentSimpleFile(string filename, byte[] content)
    {
        string persistentFileName = GetPersitentLocation(filename); // Path.GetFullPath(Path.Combine(Application.persistentDataPath, filename));
        // Debug.Log("SavePersistentSimpleFile: " + persistentFileName);

        // using (File.Create(persistentFileName)) {}
        // Debug.Log("? content: " + content.Length);

        Directory.CreateDirectory(Directory.GetParent(persistentFileName).FullName);
        File.WriteAllBytes(persistentFileName, content);
        // File.WriteAllBytes(@"C:\Users\ytchou113\AppData\LocalLow\DefaultCompany\AR-Helper2\testSaveProperty.dat", content);
        // File.WriteAllText(@"C:\Users\ytchou113\AppData\LocalLow\DefaultCompany\AR-Helper2\hi.txt", "hello, world!");
    }

    public static void SaveTransformToFile(GameObject targetObj, Transform origin, string filename, string extramode="none")
    {
        //Toolbox.ToolProperty property = Toolbox.ToolProperty.FromTransform(targetObj, origin);
        //Main.SavePersistentSimpleFile(filename, Main.FormatSerializable(property));
    }

    public static void LoadTransformFromFile(GameObject targetObj, Transform origin, string filename)
    {
        Toolbox.ToolProperty property = Main.DeserializeFormatted<Toolbox.ToolProperty>(Main.ReadPersistentSimpleFile(filename));
        Toolbox.ToolProperty.SetTransform(targetObj, property, origin);
    }

    public static void SaveToolboxManager(string foldername, ToolBoxManager manager, Transform origin)
    {
        foreach (GameObject gameObject in manager.GetMyIconPointerList())
        {
            IconPointer iconPointer = gameObject.GetComponent<IconPointer>();
            // Transform targetTransform = iconPointer.Getobj().transform; //gameObject.transform; //iconPointer.Getobj().transform;
            GameObject targetObj = iconPointer.Getobj();

            // toolproperty v1
            string filename = Path.Join(foldername, string.Format("obj{0}.toolproperty", iconPointer.Getidx()));
            SaveTransformToFile(targetObj, origin, filename);

            Debug.LogFormat("SaveToolboxManager: saved to {0}", filename);
        }
    }

    public static void LoadToolboxManager(string foldername, ToolBoxManager manager, Transform origin)
    {
        // TODO: simulate, any other way?
        string[] toolProperties = Directory.GetFiles(GetPersitentLocation(foldername)); // DirectoryNotFoundException
        List<Toolbox.ToolProperty> properties = new List<Toolbox.ToolProperty>();
        
        int maxIdx = 0;
        foreach (string propertyFile in toolProperties)
        {
            int idx = -1;
            {
                string filename = Path.GetFileName(propertyFile);

                int idxStart = "obj".Length;
                int idxEnd = filename.Length - ".toolproperty".Length;
                
                string indexStr = filename.Substring(idxStart, idxEnd - idxStart);
                if (!int.TryParse(indexStr, out idx) || !(idx >= 0))
                {
                    Debug.LogWarningFormat("Skipping invalid file {0} with index {1}", propertyFile, indexStr);
                }
            }

            maxIdx = System.Math.Max(maxIdx, idx);
        }

        if (maxIdx != toolProperties.Length - 1)
        {
            Debug.LogWarningFormat("maxIdx != toolProperties.Length - 1 (is {0} and {1})", maxIdx, toolProperties.Length - 1);
        }

        for (int i = 0; i < maxIdx + 1; i++)
        {
            manager.ArrowIconClick(); // simulate creation
        }

        var pointerList = manager.GetMyIconPointerList();
        foreach (string propertyFile in toolProperties)
        {
            int idx = -1;
            {
                string filename = Path.GetFileName(propertyFile);

                int idxStart = "obj".Length;
                int idxEnd = filename.Length - ".toolproperty".Length;
                
                string indexStr = filename.Substring(idxStart, idxEnd - idxStart);
                if (!int.TryParse(indexStr, out idx) || !(idx >= 0))
                {
                    Debug.LogWarningFormat("Skipping invalid file {0} with index {1}", propertyFile, indexStr);
                }
            }

            //Transform targetTransform = pointerList[idx].GetComponent<IconPointer>().Getobj().transform;
            GameObject targetObj = pointerList[idx].GetComponent<IconPointer>().Getobj();
            LoadTransformFromFile(targetObj, origin, propertyFile);

            Debug.LogFormat("LoadToolboxManager: loaded from {0}", propertyFile);
        }

        
    }
}
