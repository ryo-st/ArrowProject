using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    Text NowScore;
    public Text FinalScore;
    int ScoreCount=0;

    public void SetScore(int p_socre)
    {
        ScoreCount = p_socre;
        //NowScore.text = ScoreCount.ToString("D4");
        NowScore.text = ScoreCount.ToString();
        FinalScore.text = ScoreCount.ToString();
    }
    // Use this for initialization
    void Start () {
        NowScore = this.GetComponent<Text>();
        NowScore.text = ScoreCount.ToString();
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
