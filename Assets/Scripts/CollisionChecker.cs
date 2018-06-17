using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

    public bool IsContact = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") IsContact = true;
    }

}
