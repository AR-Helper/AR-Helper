using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Save all steps
    void testSaveManager()
    {
        ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
        Transform origin = GameObject.Find("TestToolboxOrigin").transform;

        Main.SaveToolboxManager("_test_manual", manager, origin);
        Debug.LogFormat("testSaveManager: saved manager to folder {0}", "_test_manual");
    }

    void testLoadManager()
    {
        ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
        Transform origin = GameObject.Find("TestToolboxOrigin").transform;

        Main.LoadToolboxManager("_test_manual", manager, origin);
        Debug.LogFormat("testSaveManager: loaded manager from folder {0}", "_test_manual");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            testSaveManager();
        }
        
        if (Input.GetKeyDown("l"))
        {
            testLoadManager();
        }
    }
}
