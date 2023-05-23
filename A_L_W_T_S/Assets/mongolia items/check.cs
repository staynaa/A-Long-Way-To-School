using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class check : MonoBehaviour
{
    public int score=0, streakScore=1;
    public int checking=0;
    private string firstBtn, secondBtn;
    private GameObject firstClick, secondClick;
    [SerializeField] GameObject panelObj;
    [SerializeField] AudioSource winSoundEffect;
    [SerializeField] AudioSource loseSoundEffect;

    [SerializeField] TextMeshProUGUI scoretext, status;
    public GameObject UiObject;

    void Start()
    {
        UiObject.SetActive(false);
    }

    void Update()
    {
    
    }
    public void checkMatch(GameObject btn){
        Debug.Log("clicked");
        if(firstClick==null){
            firstClick=btn;
            firstBtn=btn.name;
            btn.GetComponent<TMP_Text>().color= new Color(1f,1f,1f,1f);
        }
        else if(secondClick==null){
            secondClick=btn;
            secondBtn=btn.name;
            btn.GetComponent<TMP_Text>().color= new Color(1f,1f,1f,1f);
            if(firstBtn.Equals(secondBtn)){
                score+=10*streakScore;
                streakScore+=1;
                checking++;
                Debug.Log("A Match! Score= "+score);
                scoretext.SetText("Score: "+score);
                // status.color=new Color(0f,1f,0f,0f);
                status.SetText("Correct");
                winSoundEffect.Play();
                // firstClick.SetActive(false);
                // secondClick.SetActive(false);
                firstClick.GetComponent<TMP_Text>().color=new Color(1f,1f,1f,1f);
                secondClick.GetComponent<TMP_Text>().color=new Color(1f,1f,1f,1f);
                firstClick.transform.parent.GetComponent<Button>().interactable=false;
                secondClick.transform.parent.GetComponent<Button>().interactable=false;
                firstClick=null;
                secondClick=null;
                if(checking==8){
                    UiObject.SetActive(true);
                    panelObj.SetActive(false);
                  Time.timeScale=0; //game done  

                } 
            }
            else{
                score-=10;
                streakScore=1;
                scoretext.SetText("Score: "+ score);
                // status.color=new Color(1f,0f,0f,0f);
                status.SetText("Incorrect");
                loseSoundEffect.Play();
                Debug.Log("Not A Match");
                firstClick.GetComponent<TMP_Text>().color= new Color(0f,0f,0f,1f);
                secondClick.GetComponent<TMP_Text>().color= new Color(0f,0f,0f,1f);
                firstClick=null;
                secondClick=null;
            }
        }
        // btn.SetActive(false);
        // Debug.Log(btnTxt.name);

    }
}
