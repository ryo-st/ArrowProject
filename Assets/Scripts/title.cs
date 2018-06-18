using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

    GameObject Touch;
    GameObject Fadefnc;

    AudioSource TitleAudio;
    AudioSource TapAudio;

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

        TitleAudio = this.GetComponent<AudioSource>();
        TapAudio= this.gameObject.AddComponent<AudioSource>();
        TapAudio.playOnAwake = false;
        TapAudio.clip = Resources.Load<AudioClip>("Title_SE");
    }
	
	// Update is called once per frame
	void Update () {
        Flash();
        if (Input.GetMouseButton(0))
        {
            if (!CountFalg)
            {
                TitleAudio.volume *= 0.5f;
                TapAudio.Play();
            }
            CountFalg = true;

        }
        if (CountFalg == true)
        {

            count += 0.1f;
            fade d2 = Fadefnc.GetComponent<fade>();
            d2.FadeIn();
            if (count > 5f)
            {
                SceneManager.LoadScene("Main");
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