using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    //Interaction types of items 
    [SerializeField] private enum InteractionType {None,Pick_Up,Examine}

    //Initialize InteractionType Variable 
    [SerializeField] private InteractionType type;

    [Header("Exmaine")]

    //Description text of exmaine object
    [SerializeField] public string descriptionText;


    [Header("Custom Event")]

    [SerializeField] private UnityEvent customEvent;
    

  
    /* 
    Method Name: Reset()
    Description: Reset default value of required components
    */
    private void Reset() 
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    /* 
    Method Name: Interact()
    Description: Handle interaction with items 
    */
    
    public void Interact() 
    {
        switch(type)
        {
            case InteractionType.Pick_Up:

                //Add object to the pickedUpItems list
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                
                //Disable the object
                gameObject.SetActive(false);
               
                break;

            case InteractionType.Examine:
                //Call the Examine item in the interaction system
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                Debug.Log("E");
                break;

            default: 
                break;
        }

        //Invoke (call) the custom event(s)
        customEvent.Invoke();
    }   
}
