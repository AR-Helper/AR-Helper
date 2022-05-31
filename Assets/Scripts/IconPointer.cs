using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IconPointer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateObjInformation();


        if (isclicked)
        {
            clicktime += Time.deltaTime;
        }
        else 
        {
            clicktime = 0;
        }
    }

    public delegate void ButtonClick(int val);
    public ButtonClick buttonClicked;

    [SerializeField]
    GameObject _obj;
    Vector3 _localPosition;
    Quaternion _localRotation;
    Vector3 _localScale;
    int _idx;


    [Header("Control")]
    float clicktime;
    bool isclicked;

    public IconPointer(GameObject obj)
    {
        _obj = obj;
        UpdateObjInformation();
    }

    public void Setobj(GameObject obj)
    {
        _obj = obj;
        UpdateObjInformation();
    }

    public GameObject Getobj()
    {
        return _obj;
    }

    public void Setidx(int idx)
    {
        _idx = idx;
    }

    public int Getidx()
    {
        return _idx;
    }

    public void SetUsing(bool bo)
    {
        
        _obj.SetActive(bo);
    }
    void UpdateObjInformation()
    {
        if (_obj == null)
        {
            return;
        }
        _localPosition = _obj.transform.localPosition;
        _localRotation = _obj.transform.localRotation;
        _localScale = _obj.transform.localScale;
    }

    private void OnMouseDown()
    {
        isclicked = true;

    }
    private void OnMouseUp()
    {
        isclicked = false;
        if (clicktime < 0.25f)
        {
            Debug.Log("IconPointer is Clicked");
            buttonClicked(_idx);
        }
    }

    private void OnMouseDrag() 
    {
        //Debug.Log("IconPonter is Draged");
        float posY = this.gameObject.GetComponent<RectTransform>().anchoredPosition.y;
        float posX = Input.mousePosition.x - Screen.width/2;
        float minX = ToolBoxManager.toolBoxManager.GetMinBorder_IconPointer() * Screen.width;
        float maxX = ToolBoxManager.toolBoxManager.GetMaxBorfer_IconPointer() * Screen.width;
        if (posX < minX)
        {
            Debug.Log("minX"+minX);
            
            posX = minX;
        }
        else if (posX > maxX)
        {
            posX = maxX;
        }
        Vector2 pos = new Vector2(posX, posY);
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
        
    }


    
}
