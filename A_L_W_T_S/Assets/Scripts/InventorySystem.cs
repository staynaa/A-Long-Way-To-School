using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    //Inventory Item Class
    public class InventoryItem
    {
        public GameObject obj;
        public int stack =1;

        public InventoryItem(GameObject o,int s=1)
        {
            obj=o;
            stack=s;
        }
    }
    
    [Header("General Fields")]
    //List of items picked up
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

    //flag indicates if the inventory is open or not 
    [SerializeField] public bool isOpen;

    [Header("UI Items Section")]
    //Inventory System Window
    [SerializeField] private GameObject ui_window;
    [SerializeField] Image[] items_Images;
    
    [Header("UI items Description")]
    [SerializeField] GameObject ui_description_window;
    [SerializeField] Image description_Image;
    [SerializeField] Text description_Title;
    [SerializeField] Text description_Text;
   
    //consume sound effect
    [SerializeField] AudioSource consumeSoundEffect;





    private void Update() 
   {
        if(Input.GetKeyDown(KeyCode.I))
        {
         ToggleInventory();
        }
   }



    /* 
    Method Name: ToggleInventory()
    Description: Display Inventory 
    */
   void ToggleInventory()
   {
        isOpen = !isOpen;
        ui_window.SetActive(isOpen);
        Update_UI();

   }
   
   
   
   
    /* 
    Method Name: Pickup()
    Parameter: GameObject item - refrence to game item
    Description: Append detected pick up item to list 
    */
    public void PickUp(GameObject item)
    {
        //if Item is stackable
        if(item.GetComponent<Item>().stackable)
        {
            //Check if we have an exsiting item in inventory
            InventoryItem existingItem = items.Find(x=>x.obj.name==item.name);
            //if yes, stack 
            if(existingItem!=null)
            {
                existingItem.stack++;
            }
            //if no, add it as new item
            else
            {
                InventoryItem i = new InventoryItem(item);
                items.Add(i);
            }

        }
        //if item is not stackable
        else
    
        {
            InventoryItem i = new InventoryItem(item);
            items.Add(i);
        }
        Update_UI();
    }

    public bool CanPickup()
    {
        if(items.Count >=items_Images.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /* 
    Method Name: Update_UI()
    Description: Refresh the UI elements in the inventory window 
    */
    void Update_UI()
    {
        HideAll();
        //For each items in the "items" list
        //Show it in the respective slot in the "items_images"
        for (int i=0; i <items.Count;i++)
        {
            items_Images[i].sprite = items[i].obj.GetComponent<SpriteRenderer>().sprite;
            items_Images[i].gameObject.SetActive(true);
        }
    }

    /* 
    Method Name: HideAll()
    Description: Hide all the items ui images
    */
    void HideAll()
    {
        foreach(var i in items_Images)
        {
            i.gameObject.SetActive(false);
            HideDescription();
        }
    }


    /* 
    Method Name: ShowDescription()
    Parameter: int id - Reference ton item 
    Description: show item description
    */
    public void ShowDescription(int id)
    {
        //Set the image
        description_Image.sprite = items_Images[id].sprite;

        //Set the title
        //If stack == 1 write only name

         if(items[id].stack==1)
         {
            description_Title.text = items[id].obj.name;
         }
         //If stack >= 1 write  name + x stackvalue
         else
         {
            description_Title.text = items[id].obj.name + " x" + items[id].stack;
         }
        

        
       
        

        //Show the description
        description_Text.text = items[id].obj.GetComponent<Item>().descriptionText;

        //Show The element
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
         description_Text.gameObject.SetActive(true);
    }



    /* 
    Method Name: HideDescription()
    Description: hide item description
    */
    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);
        description_Text.gameObject.SetActive(false);
        

    }


    /* 
    Method Name: Consume()
    Description: Consume items that are consumable 
    */
    public void Consume(int id)
    {
        if(items[id].obj.GetComponent<Item>().type == Item.itemType.Consumables)
        {
            Debug.Log("CONSUMED");

            //Invoke the consume custom event
            items[id].obj.GetComponent<Item>().consumeEvent.Invoke();


            //Reduce the stack number
            items[id].stack--;

            //consume sound effect
            consumeSoundEffect.Play();

            //if the stack is zero
            if (items[id].stack==0)
            {
                //Destory the item in very tiny
                Destroy(items[id].obj,0.1f);
                
                //Clear the item from the list
                items.RemoveAt(id);
            }

            

            //Update UI
            Update_UI();

            




        }
    }
    
}
