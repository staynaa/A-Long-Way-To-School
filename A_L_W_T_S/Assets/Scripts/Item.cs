using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    //Interaction types of items 
    [SerializeField] private enum InteractionType {None,Pick_Up,Examine}

    //Initialize InteractionType Variable 
    [SerializeField] private InteractionType type;
  
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
                
                Debug.Log("E");
                break;
            
           
            default:
               
                break;
        }
    }   
}
