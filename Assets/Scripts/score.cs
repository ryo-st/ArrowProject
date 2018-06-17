using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    GameObject NowScre;
    public int ScoreCount;

    // Use this for initialization
    void Start () {
        NowScre = GameObject.Find("Score");
        NowScre.GetComponent<Text>().text = ScoreCount.ToString("D4");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ScoreCount += 10;
            NowScre.GetComponent<Text>().text = ScoreCount.ToString("D4");
        }
	}
}
