using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    //List of items picked up
    [SerializeField] private List<GameObject> items;

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

   }
   
   
   
   
    /* 
    Method Name: Pickup()
    Parameter: GameObject item - refrence to game item
    Description: Append detected pick up item to list 
    */
    public void PickUp(GameObject item)
    {
        items.Add(item);
        Update_UI();
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
            items_Images[i].sprite = items[i].GetComponent<SpriteRenderer>().sprite;
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

        //Set the text
        description_Title.text = items[id].name;

        //Show The element
        description_Image.gameObject.SetActive(true);
        description_Title.gameObject.SetActive(true);
    }



    /* 
    Method Name: HideDescription()
    Description: hide item description
    */
    public void HideDescription()
    {
        description_Image.gameObject.SetActive(false);
        description_Title.gameObject.SetActive(false);

    }
    
}
