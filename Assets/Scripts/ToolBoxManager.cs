using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ToolBoxManager : MonoBehaviour
{
    //加入xyz微調    
    //新增上方圈圈的拖動
    //物件更新時改變上方圈圈資訊
    [SerializeField]
    GameObject toolBox;
    [SerializeField]            //tmp
    GameObject curTarget;
    int curIconIndex;

    [SerializeField]
    GameObject Icon_Arrow;
    UnityAction myUnityAction_Arrow;
    [SerializeField]
    GameObject Template_Arrow;
    

    GameObject Icon_TextBox;
    GameObject Icon_Cancel;

    [SerializeField]
    List<GameObject> myIconPointerList;
    float minBorder_IconPointer;
    float maxBorder_IconPointer;
    float distance_IconPointer;
    [SerializeField]
    GameObject Template_IconPointer_Arrow;

    

    public static ToolBoxManager toolBoxManager;
    // Start is called before the first frame update
    void Start()
    {
        if (toolBoxManager == null)
        {
            toolBoxManager = this;
            
        }
        else 
        {
            Destroy(this);
        }

        Icon_SetEventClick();
        minBorder_IconPointer = -400f/1920f;
        maxBorder_IconPointer = 650f/1920f;
        distance_IconPointer = 150f / 1920f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIndex_IconPointer();
    }

    public void SetToolBoxVisible(bool isvisible)
    {
        toolBox.SetActive(isvisible);
        
        
    }

    public void SetCurTarget(GameObject target)
    {
        curTarget = target;
    }

    void Icon_SetEventClick()
    {
        myUnityAction_Arrow += ArrowIconClick;
        Icon_Arrow.GetComponent<ToolBoxIcon>().AddEventClick(myUnityAction_Arrow);

        
    }

    void ArrowIconClick()
    {
        GameObject tmp_icon = GameObject.Instantiate(Template_IconPointer_Arrow, Template_IconPointer_Arrow.transform.parent);

        tmp_icon.transform.localPosition = Template_IconPointer_Arrow.transform.localPosition + distance_IconPointer * Screen.width * myIconPointerList.Count * Vector3.right;//改初始位置
        tmp_icon.SetActive(true);

        GameObject tmp = GameObject.Instantiate(Template_Arrow,curTarget.transform);
        tmp.transform.localPosition = Vector3.zero;
        tmp.SetActive(true);

        tmp_icon.GetComponent<IconPointer>().Setobj(tmp);
        tmp_icon.GetComponent<IconPointer>().Setidx(myIconPointerList.Count - 1);
        tmp_icon.GetComponent<IconPointer>().buttonClicked += ShowIconPointerInList;


        myIconPointerList.Add(tmp_icon);
        ShowIconPointerInList(myIconPointerList.Count-1);
        
    }
    void ShowIconPointerInList(int val)
    {
        if(myIconPointerList.Count == 0)
        {
            return;
        }
        for(int i=0; i<myIconPointerList.Count ;i++)
        {
            if(i != val)
            {
                myIconPointerList[i].GetComponent<IconPointer>().SetUsing(false);
            }
            else{
                myIconPointerList[i].GetComponent<IconPointer>().SetUsing(true);
                curIconIndex = i;
            }
        }
    }

    public float GetMinBorder_IconPointer()
    {
        return minBorder_IconPointer;
    }
    public float GetMaxBorfer_IconPointer()
    {
        return maxBorder_IconPointer;
    }

    void CheckIndex_IconPointer()
    {
        if (myIconPointerList.Count < 2)
        {
            return;
        }
        for (int i = 0; i < myIconPointerList.Count; i++)
        {
            for (int j = i; j < myIconPointerList.Count; j++)
            {
                if (myIconPointerList[i].GetComponent<RectTransform>().anchoredPosition.x > myIconPointerList[j].GetComponent<RectTransform>().anchoredPosition.x)
                {
                    GameObject tmp = myIconPointerList[i];
                    myIconPointerList[i] = myIconPointerList[j];
                    myIconPointerList[i].GetComponent<IconPointer>().Setidx(j);
                    myIconPointerList[j] = tmp;
                    myIconPointerList[j].GetComponent<IconPointer>().Setidx(i);
                }
            }
        }
    }


}
