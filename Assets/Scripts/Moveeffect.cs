using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveeffect : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        transform.Rotate(transform.forward, 2);
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.06f);

        if (transform.position.y > 10)
        {
            transform.position = new Vector2(Random.Range(-10,10),-15);
        }
    }
}
