using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
     [SerializeField] float curTime= 80f;
    public float startingTime=10f;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject busWarning; //bus text that'll show

    void Start() {
        busWarning.SetActive(false);
    }
    void Update() {
        if(curTime>0){
            curTime -= Time.deltaTime;
        }
        else{
            curTime=0;//timer done
            SceneManager.LoadScene("youLost");//lost screen
        } 
        if(curTime<30){
            timerText.color= new Color(255,0,0);
            busWarning.SetActive(true);
        }
/*
once the time reaches 0 and the player hasn't made it to the stop, the game should let them know they lost
and ask if they'd like to retry, restarting the level.
*/
        display(curTime);
    }
    void display(float time){
        if(time < 0) time=0;
        float min= Mathf.FloorToInt(time/60);
        float sec= Mathf.FloorToInt(time%60);

        timerText.SetText("{0:00}:{1:00}", min, sec);
    }
}
