using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Main
{
    public static Main INSTANCE = new Main();

    public byte[] FormatSerializable(object serializable)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            formatter.Serialize(memoryStream, serializable);
            return memoryStream.ToArray();
        }
    }

    public Type DeserializeFormatted<Type>(byte[] formatRes)
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
    public byte[] ReadPersistentSimpleFile(string filename)
    {
        string persistentFileName = Path.Combine(Application.persistentDataPath, filename);
        return File.ReadAllBytes(persistentFileName);
    }

    public void SavePersistentSimpleFile(string filename, byte[] content)
    {
        string persistentFileName = Path.Combine(Application.persistentDataPath, filename);
        throw new System.NotImplementedException();
    }
}
