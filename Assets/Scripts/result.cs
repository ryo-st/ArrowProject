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

    private float count = 20;
    private bool PanelFlag = false;
    private float PosX, PosY, PosZ;
    private float Value;

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

        if (Input.GetKey(KeyCode.I))
        {
            PanelFlag = true;
        }
        if(PanelFlag == true)
        { 

            // リザルトパネルが下に降りる処理
            
            if (PanelFlag == true)
            {
                PosY = PosY - count;
                Panel.transform.position = new Vector3(PosX, PosY, PosZ);
                Debug.Log(PosY);
                if (PosY < 100)
                {
                    PanelFlag = false;
                }
            }
        }
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
            PosY = PosY - count;
            Panel.transform.position = new Vector3(PosX, PosY, PosZ);
            Debug.Log(PosY);
            if (PosY < 100)
            {
                PanelFlag = false;
            }
        }
    }
}
