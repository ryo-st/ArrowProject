using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour {


	void Start () {
        StartCoroutine("WaitAnimation");
    }

    private IEnumerator WaitAnimation()
    {
        Debug.Log("desf");
        yield return new WaitForSeconds(4f);
        Debug.Log("desww");
        Destroy(this.gameObject);
    }
}
