using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class NepalPauseResume : MonoBehaviour
{
 
    public GameObject PauseScreen;
    public GameObject PauseButton;
    public GameObject quiz;
 
    public static bool GameIsPaused = false;
 
    // Start is called before the first frame update
    void Start()
    {
    
    }
   
    // Update is called once per frame
    void Update()
    {

    }
 
    public void PauseGame()
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 

        PauseButton.SetActive(false);
        quiz.SetActive(false);
    }
 
    public void ResumeGame()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        PauseButton.SetActive(true);
        quiz.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}