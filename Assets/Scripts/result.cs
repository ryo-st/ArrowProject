using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    GameObject Ranking;
    GameObject Restart;

	// Use this for initialization
	void Start () {
        Ranking = GameObject.Find("ランキング");
        Restart = GameObject.Find("Restart");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
