using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class title : MonoBehaviour {

    GameObject Touch;
    GameObject Fadefnc;

    private bool FlashFlag = true;
    private bool CountFalg = false;
    private float alfa;
    private float speed = 0.038f;
    private float count;

    // Use this for initialization
    void Start()
    {
        Fadefnc = GameObject.Find("fade");
        Touch = GameObject.Find("TouchScreen");
    }
	
	// Update is called once per frame
	void Update () {
        Flash();
        if (Input.GetMouseButton(0))
        {
            CountFalg = true;
        }
        if (CountFalg == true)
        {
            count += 0.1f;
            fade d2 = Fadefnc.GetComponent<fade>();
            d2.FadeIn();
            if (count > 5f)
            {
                EditorSceneManager.LoadScene("Main2");
                CountFalg = false;
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

        if (alfa > 2 || alfa < -0.5f)
        {
            FlashFlag = !FlashFlag;
        }
        Touch.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
    }
}