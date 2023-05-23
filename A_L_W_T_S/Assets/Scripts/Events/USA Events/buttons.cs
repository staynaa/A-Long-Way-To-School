using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
    // Start is called before the first frame update
    // public Button yesBtn;
    // public Button noBtn;
    public void goToScene(string scene){
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
