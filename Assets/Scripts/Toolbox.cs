using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    [Serializable]
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vector3(UnityEngine.Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static implicit operator UnityEngine.Vector3(Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
            //     new UnityEngine.Vector3 {
            //         x = vector.x,
            //         y = vector.y,
            //         z = vector.z,
            //     }
            // ;
        }

        public override string ToString()
        {
            return 
                string.Format(
                    "{0}({1}, {2}, {3})",
                    GetType(),
                    x,
                    y,
                    z
                )
            ;
        }
    }

    [Serializable]
    class ToolProperty
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    delegate string byteArrayToStringUnaryOp(byte[] from);

    void testProperty()
    {
        ToolProperty property = 
            new ToolProperty { 
                position = new Vector3(1, 2, 3),
                rotation = new Vector3(4, 5, 6),
                scale    = new Vector3(7, 8, 9),
            }
        ;
        
        // BinaryFormatter formatter = new BinaryFormatter();
        // string formatRes = "";
        // using (MemoryStream memoryStream = new MemoryStream())
        // {
        //     formatter.Serialize(memoryStream, property);
        //     using (StreamReader reader = new StreamReader(memoryStream))
        //     {
        //         formatRes = reader.ReadToEnd();
        //     }
        // }
        
        byteArrayToStringUnaryOp base64 = (byteArray) => System.Convert.ToBase64String(byteArray);
        
        byte[] formatRes = Main.FormatSerializable(property);
        Debug.LogFormat("Serialize result: {0}", base64(formatRes));
        
        ToolProperty property1 = Main.DeserializeFormatted<ToolProperty>(formatRes);
        Debug.LogFormat("Re-serialized result: {0}", base64(Main.FormatSerializable(property1)));


        Debug.Log(property.position + " " + property1.position);
        Debug.Log(property.rotation + " " + property1.rotation);
        Debug.Log(property.scale    + " " + property1.scale   );

        // Debug.Assert(property.position == property1.position);
        // Debug.Assert(property.rotation == property1.rotation);
        // Debug.Assert(property.scale    == property1.scale   );
    }

    void testSaveProperty()
    {
        ToolProperty property = 
            new ToolProperty { 
                position = new Vector3(1, 2, 3),
                rotation = new Vector3(4, 5, 6),
                scale    = new Vector3(7, 8, 9),
            }
        ;
        
        Main.SavePersistentSimpleFile("testSaveProperty.dat", Main.FormatSerializable(property));

        ToolProperty property1 = Main.DeserializeFormatted<ToolProperty>(Main.ReadPersistentSimpleFile("testSaveProperty.dat"));

        Debug.Log(property.position + " " + property1.position);
        Debug.Log(property.rotation + " " + property1.rotation);
        Debug.Log(property.scale    + " " + property1.scale   );
    }

    // Start is called before the first frame update
    void Start()
    {
        // testProperty();
        // testSaveProperty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
