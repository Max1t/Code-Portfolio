using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardGameController : MonoBehaviour
{
    private float time = 0;
    private InputField inputFieldName;
    private Text textScores;

    // Start is called before the first frame update
    void Start()
    {
        textScores = GameObject.Find("TextScores").GetComponent<Text>();
        inputFieldName = GameObject.Find("InputFieldName").GetComponent<InputField>();
        if (AirshipStats.specialBattle) //we come to the leaderboard from winning the game
        {
            time = Time.time + AirshipStats.speedrunSavedTime;
            inputFieldName.onEndEdit.AddListener(nameWritten);
        }
        else //checking leaderboard from the menu
        {
            inputFieldName.gameObject.SetActive(false);
            ScoreKeeper scores = new ScoreKeeper();
            textScores.text = scores.PrintScores();
        }
    }

    private void nameWritten(string name)
    {
        ScoreKeeper scores = new ScoreKeeper();
        scores.SaveScore(name, time); //save the player name and score
        inputFieldName.gameObject.SetActive(false);
        textScores.text = scores.PrintScores(); //set the leaderboard results as the text
    }
}
