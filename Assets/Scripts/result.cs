using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    GameObject Panel;
    GameObject Ranking;
    GameObject Restart;

    private bool FlashFlag = true;
    private float alfa;
    private float speed = 0.035f;

    int RankUpdate = -1;            // ランク
    public static int[] PastScore = new int[5];// スコア退避用変数
    int MAxRanking = 5;// ランキングの最大数

	// Use this for initialization
	void Start () {
        Panel = GameObject.Find("Panel");
        Ranking = GameObject.Find("ランキング");
        Restart = GameObject.Find("ReStart");
	}
	
	// Update is called once per frame
	void Update () {
        Flash();
    }
    private void Flash()
    {
        if (FlashFlag == true)
        {
            alfa += speed;
        }
        else
        {
            alfa -= speed;
        }

        if (alfa > 2 || alfa < -1)
        {
            FlashFlag = !FlashFlag;
        }
        Restart.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
    }
}
