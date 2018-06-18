using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    const string FIRST_HIGH_SCORE_KEY = "firstHighScore";
    const string SECOND_HIGH_SCORE_KEY = "secondHighScore";
    const string THIRD_HIGH_SCORE_KEY = "thirdHighScore";

    int FirstHighScore, SecondHighScore, ThirdHighScore;
    public Transform ScoreRoot;
    Text NowScore;
    Text NowFinalScore, FirstHighScoreText, SecondHighScoreText, ThirdHighScoreText;
    int ScoreCount=0;

    
    public void SetScore(int p_score)
    {
        ScoreCount = p_score;
        //NowScore.text = ScoreCount.ToString("D4");
        NowScore.text = ScoreCount.ToString();
        NowFinalScore.text = ScoreCount.ToString();
    }

    public void SetFinalScore(int p_score)
    {
        ScoreCount = p_score;
        if(FirstHighScore <= ScoreCount)
        {
            ThirdHighScore = SecondHighScore;
            SecondHighScore = FirstHighScore;
            FirstHighScore = ScoreCount;
            PlayerPrefs.SetInt(FIRST_HIGH_SCORE_KEY, FirstHighScore);
            PlayerPrefs.SetInt(SECOND_HIGH_SCORE_KEY, SecondHighScore);
            PlayerPrefs.SetInt(THIRD_HIGH_SCORE_KEY, ThirdHighScore);
            PlayerPrefs.Save();

            ThirdHighScoreText.text = ThirdHighScore.ToString();
            SecondHighScoreText.text = SecondHighScore.ToString();
            FirstHighScoreText.text = FirstHighScore.ToString();
            FirstHighScoreText.transform.GetChild(0).GetComponent<Image>().enabled = true;

        }
        else if(SecondHighScore <= ScoreCount)
        {
            ThirdHighScore = SecondHighScore;
            SecondHighScore = ScoreCount;
            PlayerPrefs.SetInt(SECOND_HIGH_SCORE_KEY, SecondHighScore);
            PlayerPrefs.SetInt(THIRD_HIGH_SCORE_KEY, ThirdHighScore);
            PlayerPrefs.Save();
            ThirdHighScoreText.text = ThirdHighScore.ToString();
            SecondHighScoreText.text = SecondHighScore.ToString();
            SecondHighScoreText.transform.GetChild(0).GetComponent<Image>().enabled = true;
        }
        else if(ThirdHighScore <= ScoreCount)
        {
            ThirdHighScore = ScoreCount;
            PlayerPrefs.SetInt(THIRD_HIGH_SCORE_KEY, ThirdHighScore);
            PlayerPrefs.Save();
            ThirdHighScoreText.text = ThirdHighScore.ToString();
            ThirdHighScoreText.transform.GetChild(0).GetComponent<Image>().enabled = true;

        }


    }
    void Start () {

        FirstHighScore = PlayerPrefs.GetInt(FIRST_HIGH_SCORE_KEY, 0);
        SecondHighScore = PlayerPrefs.GetInt(SECOND_HIGH_SCORE_KEY, 0);
        ThirdHighScore = PlayerPrefs.GetInt(THIRD_HIGH_SCORE_KEY, 0);

        NowScore = this.GetComponent<Text>();
        NowScore.text = ScoreCount.ToString();
;
        int ObjectCount = 0;
        GameObject[] Scores= new GameObject[ScoreRoot.childCount];
        foreach (Transform ScoreUI in ScoreRoot)
        {
            Scores[ObjectCount] = ScoreUI.gameObject;
            ObjectCount++;
        }
        NowFinalScore = Scores[0].GetComponent<Text>();
        FirstHighScoreText = Scores[1].GetComponent<Text>();
        SecondHighScoreText = Scores[2].GetComponent<Text>();
        ThirdHighScoreText = Scores[3].GetComponent<Text>();

        ThirdHighScoreText.text = ThirdHighScore.ToString();
        SecondHighScoreText.text = SecondHighScore.ToString();
        FirstHighScoreText.text = FirstHighScore.ToString();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ScoreCount += 10;
            NowScore.text = ScoreCount.ToString();
        }
	}
}
