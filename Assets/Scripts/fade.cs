using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{

    GameObject Fade;
    private bool FadeInFlag = false;
    private bool FadeOutFlag = false;
    private float speed = 0.01f;
    private float alfa;

    // Use this for initialization
    void Start()
    {
        this.Fade = GameObject.Find("fade");
    }

    // Update is called once per frame
    void Update()
    {
        if (FadeInFlag == true)
        {
            FadeIn();
        }
        if (FadeOutFlag == true)
        {
            FadeOut();
        }
        
    }

    public void FadeIn()
    {
        FadeInFlag = true;
        if (FadeInFlag == true)
        {
            Fade.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
            alfa += speed;
            if (alfa >= 1)
            {
                FadeInFlag = false;
                alfa = 0;
            }
        }
    }
    public void FadeOut()
    {
        FadeOutFlag = true;
        if (FadeOutFlag == true)
        {
            Fade.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
            alfa -= speed;
            if (alfa <= 0)
            {
                FadeOutFlag = false;
                alfa = 1;
            }
        }
    }
}