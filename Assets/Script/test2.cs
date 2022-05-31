using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ARTrackedObjectManager manager = GameObject.Find("UnityEngine.XR.ARFoundation").GetComponent<ARTrackedObjectManager>();
        manager.trackedObjectsChanged += onTrackedObjectsChanged;
    }

    void onTrackedObjectsChanged(ARTrackedObjectsChangedEventArgs args)
    {
        Debug.LogFormat("tracked object changed: {}", args.added);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
