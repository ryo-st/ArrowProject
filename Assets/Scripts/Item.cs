using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    void Start () {
        StartCoroutine("ItemAnimation");
    }

    private void Update()
    {
        if (MoveEnemy.GameEnd) this.gameObject.SetActive(false);
    }
    float rotateSpeed = 15.0f;
    private IEnumerator ItemAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            while (Mathf.DeltaAngle(transform.eulerAngles.z, 180) > 1f)
            {
                transform.Rotate(new Vector3(0f, 0f, rotateSpeed));
                yield return null;
            }
            yield return new WaitForSeconds(2f);
            while (Mathf.DeltaAngle(transform.eulerAngles.z, 360) > 1f)
            {
                transform.Rotate(new Vector3(0f, 0f, rotateSpeed));
                yield return null;
            }
        }
    }
    public bool IsSetting = false;
    public bool IsContact = false;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsContact = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine("ItemCoolTime");
        }
    }

    public void ItemInitialize()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        IsSetting = true;
    }

    public bool IsCoolTime = false;
    private IEnumerator ItemCoolTime()
    {
        IsCoolTime = true;
        yield return new WaitForSeconds(5f);
        IsCoolTime = false;
    }
}
