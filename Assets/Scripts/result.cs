using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    Image Restart;

    RectTransform Panel;

    private bool FlashFlag = true;
    private float alfa;
    private float speed = 0.035f;

    private float count = 20;
    private bool PanelFlag = false;
    private float PosX, PosY;
    private float TargetPosY;
	// Use this for initialization
	void Start () {
        Restart = GameObject.Find("ReStart").GetComponent<Image>();
        Panel = GameObject.Find("Panel").GetComponent<RectTransform>();

        PosX = Panel.anchoredPosition.x;
        PosY = Panel.anchoredPosition.y;
        TargetPosY = (Panel.anchoredPosition.y - Panel.sizeDelta.y) * 1.5f;
        TargetPosY = -375;//CenterPosition,MagicNumber
        //Panel.anchoredPosition = new Vector2(Panel.anchoredPosition.x ,(Panel.anchoredPosition.y- Panel.sizeDelta.y)*1.5f);
    }

    float ResultAnimationWaitTime = 0;
	void Update () {
        //for Debug
        //if (Input.GetKey(KeyCode.I))
        //{
        //    Result();
        //}
        // ゲームに戻る処理
        if (MoveEnemy.GameEnd)
        {
            ResultAnimationWaitTime += Time.deltaTime;
            if (ResultAnimationWaitTime > 2 && Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
 
    void FixedUpdate()
    {
        Flash();
        if (PanelFlag == true)
        {
            PosY = PosY - count;
            Panel.anchoredPosition = new Vector2(PosX , PosY);
            if (PosY < TargetPosY)
            {
                PanelFlag = false;
            }
        }
    }
    // ReStartの点滅処理
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
        Restart.color = new Color(1, 1, 1, alfa);
    }

    public AudioSource ResultSource;
    // リザルト画面が呼び出される
    public void Result()
    {
        //ResultSource.Play();
        // リザルトパネルが下に降りる処理
        PanelFlag = true;
    }
}
