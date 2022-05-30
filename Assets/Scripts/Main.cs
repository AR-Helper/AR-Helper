using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Main
{
    public static Main INSTANCE = new Main();

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

    // public FileStream GetPersistentFileStream(string filename)
    // {
    //     throw new System.NotImplementedException();
    // }
    public static byte[] ReadPersistentSimpleFile(string filename)
    {
        string persistentFileName = Path.GetFullPath(Path.Combine(Application.persistentDataPath, filename));
        // Debug.Log("ReadPersistentSimpleFile: " + persistentFileName);
        
        return File.ReadAllBytes(persistentFileName);
    }

    public static void SavePersistentSimpleFile(string filename, byte[] content)
    {
        string persistentFileName = Path.GetFullPath(Path.Combine(Application.persistentDataPath, filename));
        // Debug.Log("SavePersistentSimpleFile: " + persistentFileName);

        using (File.Create(persistentFileName)) {}
        // Debug.Log("? content: " + content.Length);
        
        File.WriteAllBytes(persistentFileName, content);
        // File.WriteAllBytes(@"C:\Users\ytchou113\AppData\LocalLow\DefaultCompany\AR-Helper2\testSaveProperty.dat", content);
        // File.WriteAllText(@"C:\Users\ytchou113\AppData\LocalLow\DefaultCompany\AR-Helper2\hi.txt", "hello, world!");
    }
}
