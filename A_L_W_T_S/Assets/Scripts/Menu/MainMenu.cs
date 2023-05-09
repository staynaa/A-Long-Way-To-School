using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InputField playerName;
    //Start Button: Loads the next level in the scene builder queue
    public void StartGame()
    {
        string s = playerName.text;
        PersistentData.Instance.SetName(s);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //Quit Button: Closes the application
    public void Quit()
    {
        Debug.Log ("Quit Game!");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    //Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {

    }
}

