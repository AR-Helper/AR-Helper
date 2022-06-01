using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("detected");
        //Debug.Log("detected!"+this.transform.position + " " + this.transform.lossyScale + " " + this.transform.localScale + ", " + this.transform.parent.gameObject + " " + this.transform.parent.parent.gameObject);
    }
}
