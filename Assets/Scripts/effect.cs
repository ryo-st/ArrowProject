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
    private float alfa = 1;

	// Use this for initialization
	void Start () {
       Sprite[] bgef = Resources.LoadAll<Sprite>("bgef");
        for (int i = 0; i < 9; i++)
        {
            Effect[i] = Instantiate(PrefabEffect, new Vector2(0,10), Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        }

        for (int i = 0; i < 9; i++)
        {
            Effect[i].GetComponent<SpriteRenderer>().sprite = bgef[i];
        }
      
    }
	
	// Update is called once per frame
	void Update () {
        Value += 0.1f;
        if (Value >= 3)
        {
            EffectController();
            Value = 0;
        }
  
    }
    private void EffectController()
    {
        Effect[Random.Range(0, 9)].transform.position = new Vector2(Random.Range(-10, 10), -10);

        Effect[Random.Range(0, 9)].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alfa);
        alfa -= 0.05f;
        if (alfa < 0)
        {
            alfa = 1;
        }

    }
}
