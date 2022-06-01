using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextGUI : MonoBehaviour
{
    private TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void OnGUI()
    {
        //stringToEdit = GUI.TextField(new Rect(10,10,200,30),stringToEdit,30);
        GUI.skin.button.fontSize = 40;
        // // if (GUILayout.Button("image")){
        // //     keyboard = TouchScreenKeyboard.Open("hello",TouchScreenKeyboardType.Default);
        // }
        if (GUI.Button(new Rect(100, 900, 500, 80), "please tap here"))
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

            ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
            List<GameObject> pointerList = manager.GetMyIconPointerList();

            Debug.LogFormat("ONGUI: manager {0}, pointerList {1}", manager, pointerList);

            if (pointerList.Count == 0)
            {
                return;
            }

            GameObject placeObject = pointerList[manager.curIconIndex].GetComponent<IconPointer>().Getobj();
            Debug.LogFormat("ONGUI: placeObject {0}", placeObject);

            Debug.LogFormat("ONGUI: textUI {0}", placeObject.transform.Find("Canvas/Text"));
            Debug.LogFormat("ONGUI: textComp {0}", placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>());
            Debug.LogFormat("ONGUI: text {0}", placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text);

            keyboard.text = placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text;
        }
        //Debug.Log(keyboard.text);

        if (keyboard != null && keyboard.active)
        {
            Debug.LogFormat("ONGUI: post keyboard");

            ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
            List<GameObject> pointerList = manager.GetMyIconPointerList();

            Debug.LogFormat("ONGUI: manager {0}, pointerList {1}", manager, pointerList);

            if (pointerList.Count == 0)
            {
                return;
            }

            GameObject placeObject = pointerList[manager.curIconIndex].GetComponent<IconPointer>().Getobj();
            Debug.LogFormat("ONGUI: placeObject {0}", placeObject);

            Debug.LogFormat("ONGUI: textUI {0}", placeObject.transform.Find("Canvas/Text"));
            Debug.LogFormat("ONGUI: textComp {0}", placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>());
            Debug.LogFormat("ONGUI: text {0}", placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text);
            placeObject.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = keyboard.text;
        }
    }
}
