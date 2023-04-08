using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
   

    [Tooltip("Reference to object being used as \"interactChecker\"")]
    //Detection point of object
    [SerializeField] private Transform detectionPoint;
    
    //Detection radius of object
    private const float detectionRadius = 0.2f;

    //Detection layer of object
    [SerializeField] private LayerMask detectionLayer;

   
   
   
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
                Debug.Log("Interact");
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
        return Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer);

    }

}
