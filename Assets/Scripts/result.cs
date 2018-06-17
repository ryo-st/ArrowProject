using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    GameObject Panel;
    GameObject Restart;

    private bool FlashFlag = true;
    private float alfa;
    private float speed = 0.035f;

    private float count = 1;
    private bool PanelFlag = false;
    private float PosX, PosY, PosZ;

	// Use this for initialization
	void Start () {
        Panel = GameObject.Find("Panel");
        Restart = GameObject.Find("ReStart");

        PosX = Panel.transform.position.x;
        PosY = Panel.transform.position.y;
        PosZ = Panel.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        Flash();

        // ゲームに戻る処理
        if (Input.GetMouseButton(0))
        {
            EditorSceneManager.LoadScene("Main2");
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
        Restart.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
    }

    // リザルト画面が呼び出される
    public void Result()
    {
        // リザルトパネルが下に降りる処理
        PanelFlag = true;
        if (PanelFlag == true)
        {
            Panel.transform.position = new Vector3(PosX, PosY - count, PosZ);
            PosY = PosY - count;
            if (PosY < 1)
            {
                PanelFlag = false;
            }
        }
    }
}
