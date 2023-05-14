using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScores : MonoBehaviour
{
    const int NUM_HIGH_SCORES = 5;
    const string NAME_KEY = "John Doe";
    const string SCORE_KEY = "0";

    [SerializeField] string playerName;
    [SerializeField] int playerScore;

    [SerializeField] TextMeshProUGUI[] nameTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;


    // Start is called before the first frame update
    void Start()
    {
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();

        SaveHighScores();

        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            Debug.Log(i + " " + PlayerPrefs.GetString(NAME_KEY + i) + " ");
            Debug.Log(i + " " + PlayerPrefs.GetInt(SCORE_KEY + i) + " ");
        }

        ShowHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SaveHighScores()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            Debug.Log(i + " " + PlayerPrefs.GetString(NAME_KEY + i) + " ");
            Debug.Log(i + " " + PlayerPrefs.GetInt(SCORE_KEY + i) + " ");
        }

        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                if (playerScore > currentScore)
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);

                    playerName = tempName;
                    playerScore = tempScore;

                }
            }
            else
            {
                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                return;
            }
        }
    }

    public void ShowHighScores()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            Debug.Log(i + " ");
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY + (i + 1));
            scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY + (i + 1)).ToString();
        }
    }
}
