using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollector : MonoBehaviour
{
    private double totalCash = 0.0;
    private void OnTriggerEnter2D(Collider2D collision) 
    {

     if(collision.gameObject.CompareTag("Penny"))
     {
        Destroy(collision.gameObject);
        totalCash += 0.1;
     }
     
     else if (collision.gameObject.CompareTag("Nickel"))
     {
        Destroy(collision.gameObject);
        totalCash += 0.5;
     }
     
     else if (collision.gameObject.CompareTag("Quarter"))
     {
        Destroy(collision.gameObject);
        totalCash += 0.25;
        
     }



    
     }
}
