using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedReferenceObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
        manager.SetCurTarget(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
