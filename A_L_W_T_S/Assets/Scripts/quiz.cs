using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class quiz : MonoBehaviour
{
    [SerializeField] Image flagImg, statusBox;
    [SerializeField] string correctText,opt1,opt2,opt3;
    public List<Sprite> flags = new List<Sprite>();
    private List<int> alreadyAsked=new List<int>();
    [SerializeField] List<TextMeshProUGUI> choicesBtn=new List<TextMeshProUGUI>();
    [SerializeField] TextMeshProUGUI scoreText, statusText;
    public int score=0, quesNum=1,streakScore=1;
    [SerializeField] GameObject curFlag;
    [SerializeField] private AudioSource winSoundEffect;
    [SerializeField] private AudioSource loseSoundEffect;
    void Start()
    {
        flagImg= curFlag.GetComponent<Image>(); //get image component
        nextFlag();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextFlag(){
        int i1=getRand(),i2=getRand(),i3=getRand(),i4=getRand();
        while(((i1==i2)||(i2==i3)||(i3==i4)||(i4==i1)||(i3==i1)||(i4==i2))||(alreadyAsked.Contains(i1))){
            i1=getRand(); //the correct answer
            i2=getRand();
            i3=getRand();
            i4=getRand();
        }
        flagImg.sprite = flags[i1];
        correctText=flagImg.sprite.name;
        opt1=flags[i2].name;
        opt2=flags[i3].name;
        opt3=flags[i4].name;
        alreadyAsked.Add(i1);
        if(alreadyAsked.Count==flags.Count) alreadyAsked.Clear();

        int x1=Random.Range(0,4),x2=Random.Range(0,4),x3=Random.Range(0,4),x4=Random.Range(0,4);
        while((x1==x2)||(x2==x3)||(x3==x4)||(x3==x1)||(x4==x1)||(x4==x2)){
            x1=Random.Range(0,4);
            x2=Random.Range(0,4);
            x3=Random.Range(0,4);
            x4=Random.Range(0,4);
        }
        choicesBtn[x1].SetText(correctText);
        choicesBtn[x1].name=correctText;

        choicesBtn[x2].SetText(opt1);
        choicesBtn[x2].name=opt1;

        choicesBtn[x3].SetText(opt2);
        choicesBtn[x3].name=opt2;

        choicesBtn[x4].SetText(opt3);
        choicesBtn[x4].name=opt3;
    }
    public int getRand(){
        return Random.Range(0,flags.Count); //index for flags
    }

    public void checkAnswer(GameObject choice){
        string ans=choice.name;
        Debug.Log("ANSWER: "+correctText+" Your Answer: "+ans);
        if(correctText.Equals(ans)){
            score+=10*streakScore;
            Debug.Log("CORRECT");
            statusText.SetText("Correct!");
            statusBox.GetComponent<Image>().color = Color.green;
            streakScore+=2; //gain streak score
            winSoundEffect.Play();
        }
        else{
            score-=20;
            Debug.Log("INCORRECT");
            statusText.SetText("Incorrect!");
            statusBox.GetComponent<Image>().color = Color.red;
            streakScore=1; //lose streak score
            loseSoundEffect.Play();
        }
        scoreText.SetText("Score: "+score);
        quesNum++;

        nextFlag();
    }
}
