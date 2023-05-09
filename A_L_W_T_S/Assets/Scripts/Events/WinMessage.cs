using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMessage : MonoBehaviour
{
    public GameObject UiObject;
    public GameObject trigger;
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
       UiObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){ 
            UiObject.SetActive(true); 
            Time.timeScale = 0f;
            GameIsPaused = true; 

        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
}
