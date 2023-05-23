using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class NepalInstructions : MonoBehaviour
{
 
    public GameObject InstructionScreen;
    public GameObject ContinueButton;
    public GameObject quiz;
 
    public static bool GameIsPaused = false;
 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        quiz.SetActive(false);
    }
   
    // Update is called once per frame
    void Update()
    {
        //quiz.SetActive(true);
    }
 
    public void InstructScreen()
    {
        InstructionScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 

        ContinueButton.SetActive(true);
        quiz.SetActive(false);
    }
 
    public void ContinueGame()
    {
        InstructionScreen.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        ContinueButton.SetActive(false);
        quiz.SetActive(true);
    }
}