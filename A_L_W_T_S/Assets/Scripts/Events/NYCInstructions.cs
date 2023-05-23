using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class NYCInstructions : MonoBehaviour
{
 
    public GameObject InstructionScreen;
    public GameObject ContinueButton;
    public GameObject distanceTracker;
    public GameObject pauseButton;

 
    public static bool GameIsPaused = false;
 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        distanceTracker.SetActive(false);
        pauseButton.SetActive(false);

    }
   
    // Update is called once per frame
    void Update()
    {

    }
 
    public void InstructScreen()
    {
        InstructionScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 

        ContinueButton.SetActive(true);
        distanceTracker.SetActive(false);
        pauseButton.SetActive(false);
    }
 
    public void ContinueGame()
    {
        InstructionScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        ContinueButton.SetActive(false);
        distanceTracker.SetActive(true);
        pauseButton.SetActive(true);
    }
}