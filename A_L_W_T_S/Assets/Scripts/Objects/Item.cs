using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

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

    [SerializeField] public bool stackable = false;

    [Header("Exmaine")]
    //Description text of exmaine object
    [SerializeField] public string descriptionText;


    [Header("Custom Event")]

    [SerializeField] private UnityEvent customEvent;

    [SerializeField] public UnityEvent consumeEvent;

    [SerializeField] public static int score = 0;
    public int scoreOverall;
    [SerializeField] public TextMeshProUGUI scoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        scoreOverall= PersistentData.Instance.GetScore();
        scoreTxt.text = "Score:" + score;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "Score:" + score;

    }
  
    /* 
    Method Name: Reset()
    Description: Reset default value of required components
    */
    private void Reset() 
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public static void ResetScore()
    {
        score = 0;
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

                if(!FindObjectOfType<InventorySystem>().CanPickup())
                {
                    return;
                }

                //Add object to the pickedUpItems list
                FindObjectOfType<InventorySystem>().PickUp(gameObject);
                
                //Disable the object
                gameObject.SetActive(false);

                score=score+10;
                PersistentData.Instance.SetScore(score+scoreOverall);
               
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
