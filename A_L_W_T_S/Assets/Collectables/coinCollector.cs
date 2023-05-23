using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class coinCollector : MonoBehaviour
{
    private double fareTotal = 0;
    [SerializeField] private TMP_Text fareText;
    


    private void OnTriggerEnter2D(Collider2D collision) 
    {

     if(collision.gameObject.CompareTag("Penny"))
     {
        Destroy(collision.gameObject);
        fareTotal += 0.1;
     }
     
     else if (collision.gameObject.CompareTag("Nickel"))
     {
        Destroy(collision.gameObject);
        fareTotal += 0.5;
     }
     
     else if (collision.gameObject.CompareTag("Quarter"))
     {
        Destroy(collision.gameObject);
        fareTotal += 0.25;
        
     }

      fareText.text = "Fare: $" + fareTotal;
     }
}
