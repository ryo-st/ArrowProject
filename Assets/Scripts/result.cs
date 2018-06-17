using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    GameObject Panel;
    GameObject Ranking;
    GameObject Restart;

    int RankUpdate = -1;            // ランク
    public static int[] PastScore = new int[5];// スコア退避用変数
    int MAxRanking = 5;// ランキングの最大数

	// Use this for initialization
	void Start () {
        Panel = GameObject.Find("Panel");
        Ranking = GameObject.Find("ランキング");
        Restart = GameObject.Find("Restart");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
