using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToolBoxIcon : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityAction mButtonClick;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddEventClick(UnityAction unityAction)
    {
        mButtonClick += unityAction;
        
        
    }

    private void OnMouseDown()
    {
        
        Debug.Log("Tool Clicked");
        mButtonClick();
    }




}
