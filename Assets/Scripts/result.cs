using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// リザルトのパネルの処理

public class result : MonoBehaviour {

    GameObject Panel;
    GameObject Restart;

    private bool FlashFlag = true;
    private float alfa;
    private float speed = 0.035f;

    private float count = 30;
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

       // D押したらパネルが下に降りる
        if (Input.GetKey(KeyCode.D))
        {
            PanelFlag = true;
        }
        // リザルトパネルが下に降りる処理
        if (PanelFlag == true)
        {
            Panel.transform.position = new Vector3(PosX, PosY - count, PosZ);
            PosY = PosY - count;
            if (PosY < 140)
            {
                PanelFlag = false;
            }
        }
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
