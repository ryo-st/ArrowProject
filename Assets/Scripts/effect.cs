using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect : MonoBehaviour {

    public GameObject PrefabEffect;
    private GameObject[] Effect = new GameObject[9];
    private Vector2 Pos;
    private int RandomCount;
    private float RandPosX;
    private float HorizontallLength;

    private float Value = 0.1f;

	// Use this for initialization
	void Start () {
       Sprite[] bgef = Resources.LoadAll<Sprite>("bgef");
        for (int i = 0; i < 9; i++)
        {
            Effect[i] = Instantiate(PrefabEffect, Pos, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        }
        Camera camera = Camera.main;
        Vector2 TopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 BottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

        HorizontallLength = Mathf.Abs(TopLeft.x) + Mathf.Abs(BottomRight.x);
        Debug.Log(HorizontallLength);
        RandomCount = Random.Range(0, 9);
        RandPosX = Random.Range(-10, 10);
        for (int i = 0; i < 9; i++)
        {
            Effect[i].GetComponent<SpriteRenderer>().sprite = bgef[RandomCount];
        }
      
    }
	
	// Update is called once per frame
	void Update () {
        Value += 0.1f;

        for (int i = 0; i < 9; i++)
        {
            Effect[i].transform.position = new Vector2(RandPosX, Pos.y + Value);

            if (Value > 10)
            {
                Value = -15;
                RandPosX = Random.Range(-10, 10);

            }
            if (Value < -10)
            {
                Effect[i].transform.position = new Vector2(RandPosX, Value);
            }
        }
        
    }
}
