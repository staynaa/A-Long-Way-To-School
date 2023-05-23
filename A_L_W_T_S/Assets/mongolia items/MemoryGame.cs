using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryGame : MonoBehaviour
{
    // public int score=0;
    // public GameObject firstBtn, secondBtn;
    // public GameObject firstClick, secondClick;
    public List<TextMeshProUGUI> tilesText = new List<TextMeshProUGUI>();
    private List<int> called=new List<int>(); 
    //9,30,36,100,24,72,10,55
    public string[] mathQues={"5+4","3+6","15+15","16+14","12x3","26+10","55+45","37+63","4x6","8x3","61+11","8x9","100÷10","2x5","5x11","27+28"};
    public string[] val={"9","9","30","30","36","36","100","100","24","24","72","72","10","10","55","55"};
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<mathQues.Length;i++){
            int idx=getRand();
            // Debug.Log("index= "+idx);
            tilesText[idx].SetText(mathQues[i]);
            tilesText[idx].name=val[i];
        }
    }
    //shuffle
    public int getRand(){
        int index=Random.Range(0,mathQues.Length); //index
        while(called.Contains(index)){
            index=Random.Range(0,mathQues.Length);
        }
        called.Add(index);
        return index;
    }
    public void hello(){
        Debug.Log("Hello");
    }
    // public void checkMatch(GameObject btnTxt, GameObject btn){
    //     if(firstClick==null){
    //         firstClick=btnTxt;
    //         firstBtn=btn;
    //     }
    //     else if(secondClick==null){
    //         secondClick=btnTxt;
    //         secondBtn=btn;
    //         if(firstClick.Equals(secondClick)){
    //             score++;
    //             Debug.Log("A Match! Score= "+score);
    //             firstBtn.SetActive(false);
    //             secondBtn.SetActive(false);
    //         }
    //         else{
    //             Debug.Log("Not A Match");
    //         }
    //     }

    // }
}
//"9","9","30","30","36","36","100","100","24","24","72","72","10","10","55","55"

//check if first is null, if null set to text's name
// check if second is null, if null set to text's name
//compare
//if equal add score and setactive false to both buttons.