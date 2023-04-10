using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    //Interaction types of items 
    [SerializeField] private enum InteractionType {None,Pick_Up,Examine}

    // item type of items
    [SerializeField] public enum itemType {Static,Consumables}

    [Header("Attributes")]
    //Initialize InteractionType Variable 
    [SerializeField] private InteractionType interactType;

    //Initialize itemType Variable 
    [SerializeField] public itemType type;

    [Header("Exmaine")]
    //Description text of exmaine object
    [SerializeField] public string descriptionText;


    [Header("Custom Event")]

    [SerializeField] private UnityEvent customEvent;

    [SerializeField] public UnityEvent consumeEvent;
    

  
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
        switch(interactType)
        {
            case InteractionType.Pick_Up:

                //Add object to the pickedUpItems list
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                
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
