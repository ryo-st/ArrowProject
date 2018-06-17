using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

    public bool IsContact = false;
	void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") IsContact = true;
	}

}
