using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busArrival : MonoBehaviour
{
    public GameObject UiObject;
    public GameObject trigger;
    // Start is called before the first frame update
    void Start()
    {
       UiObject.SetActive(false);// shouldn't be able to see text when game starts
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){ //change to whatever tag connected to player
            UiObject.SetActive(true); //if wall is touched, will notify player that they are going out of bounds
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
