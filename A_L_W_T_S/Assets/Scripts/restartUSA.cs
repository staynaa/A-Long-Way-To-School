using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartUSA : MonoBehaviour
{
    public GameObject player;
    public GameObject killzone;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){ //change to whatever tag connected to player
            SceneManager.LoadScene("USA-Level");
        }
    }
}
