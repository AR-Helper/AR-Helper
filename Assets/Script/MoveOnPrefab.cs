using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class MoveOnPrefab : MonoBehaviour
{
    [SerializeField]
    private Vector2 touchPosition = default;
    [SerializeField]
    private Camera arCamera;
    private ARRaycastManager arRaycastManager;

    //public GameObject placeObject;
    public GameObject placePrefab;

    private bool onTouch = false;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //public GameObject arraw;
    private ARTrackedImageManager imagemanager;
    private List<ARTrackedImage> images = new List<ARTrackedImage>();

    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    
    }
    private void Start()
    {
        imagemanager = this.GetComponent<ARTrackedImageManager>();
        //imagemanager.


        foreach (ARTrackedImage image in imagemanager.trackables)
        {
            Debug.Log("searchimagetrackable "+ image);
            images.Add(image);
        }
        //Debug.Log("image" + images[0]);
    }
    // Update is called once per frame
    void Update()
    {
        //RaycastHit hitObject = null;
        bool hitImage = false;
        if (Input.touchCount > 0)
        {
            //caculate the screen and the 3d world's relation
            Touch touch = Input.GetTouch(0); 
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                //Began/Moved/Ended
                //began : initial touch
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                Debug.Log("touch");
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    //hitObject.transform.

                    //foreach (object obj in hitObject.transform.gameObject.GetComponents<object>())
                    //{
                    //    Debug.Log("onTouch components : " + obj);
                    //}
                 //   Debug.Log("onTouch : "+ hitObject.transform.gameObject.GetComponents<object>()); //hitObject.Equals(images[0]));
                    if(hitObject.transform.GetComponent<ARTrackedImage>() != null)
                    {
                        hitImage = true;
                        //Debug.Log("yesssssssss");   
                    }
                    onTouch = true;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                onTouch = false;
            }
            
            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                //calculate the real 3d world
                ARRaycastHit hit = hits[0];
               
                //Debug.Log("hittype : " + hit.trackableId);
                Pose hitpose = hits[0].pose;

                ToolBoxManager manager = GameObject.Find("ToolBoxManager").GetComponent<ToolBoxManager>();
                List<GameObject> pointerList = manager.GetMyIconPointerList();

                if (pointerList.Count == 0)
                {
                    return;
                }

                GameObject placeObject = pointerList[manager.curIconIndex].GetComponent<IconPointer>().Getobj();
                //if (placeObject == null)
                //{
                //    placeObject = Instantiate(placePrefab, hitpose.position, Quaternion.identity);
                //}
                //else
                {
                    if (onTouch)
                    {
                        //Debug.Log("touchOnBall"+hitpose.position);
                        if(hitImage)
                        {

                            Debug.Log("setArrawPos");
                            placeObject.transform.position = hitpose.position;
                        }
                        
                    }
                }
            }
           

        }

    }

}
