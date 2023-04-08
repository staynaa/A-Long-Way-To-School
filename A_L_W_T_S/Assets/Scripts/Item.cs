using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
  
  [SerializeField] private enum InteractionType {None,Pick_Up,Examine}
  [SerializeField] private InteractionType type;
  
    private void Reset() 
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }


    public void Interact()
    
    {
        switch(type)
        {
            case InteractionType.Pick_Up:
                Debug.Log("P");
                break;
            
            case InteractionType.Examine:
                Debug.Log("E");
                break;
            
            default:
                break;
        }
    }
    
}
