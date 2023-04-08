using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
   
    [Header("Detection Parameters")]
    [Tooltip("Reference to object being used as \"interactChecker\"")]
    //Detection point of object
    [SerializeField] private Transform detectionPoint;
    
    //Detection radius of object
    private const float detectionRadius = 0.2f;

    //Detection layer of object
    [SerializeField] private LayerMask detectionLayer;

    //Cache Trigger Object
    [SerializeField] private GameObject detectObject;

    [Header("Others")]
    //List of picked items
    [SerializeField] private List<GameObject> pickedItems = new List<GameObject>();

   
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectObject())
        {   
            if(InteractInput())
            {
                detectObject.GetComponent<Item>().Interact();
            }
        }
    }


    /* 
    Method Name: InteractInput()
    Description: return true if "E" Key is pressed/helded and return 
    false if otherwise
    */  
    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }


    /* 
    Method Name: DetectObject()
    Description: return true if object is detected near player and return 
    false if otherwise
    */
    bool DetectObject()
    {
        // Set obj to refrence of object detected 
        Collider2D obj = 
        Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer);

        if(obj == null)
        {
            return false;
        }
        else
        {
            detectObject = obj.gameObject;
            return true;
        }
       

    }


    /* 
    Method Name: PickupItem()
    Description: Append detected pick up item to list 
    */
    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
    }

}
