using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPlayerChecker : MonoBehaviour {

    public bool IsContact = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall") IsContact = true;
        Debug.Log("enter"+ IsContact);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wall") IsContact = true;
        Debug.Log("stay"+ IsContact);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Wall") IsContact = false;
        Debug.Log("exit"+ IsContact);
    }
}
