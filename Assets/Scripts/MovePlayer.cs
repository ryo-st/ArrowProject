using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの移動処理
// ※マウスの移動にたいして動くだけ
public class MovePlayer : MonoBehaviour {

    private Vector3 playerPos;
    private Vector3 mousePos;
    private float PlayerSpeed = 10f;

    public class Player
    {
        ProgressPlayerChecker ProgressPlayerCheck;
        public Player(){}
        public bool GetIsContact()
        {
            return ProgressPlayerCheck.IsContact;
        }
        public void InitialIsContact() { ProgressPlayerCheck.IsContact = false; }
    }

    float VerticalLength;
    float HorizontallLength;
    public Player player;
    private void Start()
    {
        player = new Player();
        thisBody = this.GetComponent<Rigidbody2D>();

        Vector2 TopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 BottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        VerticalLength = Mathf.Abs(TopLeft.y) + Mathf.Abs(BottomRight.y);
        HorizontallLength = Mathf.Abs(TopLeft.x) + Mathf.Abs(BottomRight.x);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerPos = this.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.touchSupported)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

        }
    }
    Rigidbody2D thisBody;
    private void FixedUpdate()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 NowPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // タッチ入力をサポートしてるかどうからしい
            if (Input.touchSupported)
            {
                NowPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            playerPos.z = mousePos.z = NowPos.z = 0.0f;
            Vector3 diff = NowPos - mousePos;
            diff.x /= HorizontallLength;
            diff.y /= VerticalLength;


            
            thisBody.velocity = diff * PlayerSpeed;
            /*
            if (0 < dist && dist < 7)
            {
                Debug.Log("[0-7]dist:" + dist);
                this.transform.position = playerPos + diff * 0.35f;
            }
            else if (dist < 10)
            {
                Debug.Log("[7-10]dist:" + dist);
                this.transform.position = playerPos + diff * 0.55f;
            }
            else if (dist < 13)
            {
                Debug.Log("[10-13]dist:" + dist);
                this.transform.position = playerPos + diff * 0.75f;
            }
            else
            {
                Debug.Log("[10-13]dist:" + dist);
                this.transform.position = playerPos + diff * 0.95f;

            }*/

        }
        if (Input.GetMouseButtonUp(0))
        {
            playerPos = Vector3.zero;
            mousePos = Vector3.zero;
            thisBody.velocity = Vector2.zero;
        }
    }
    //bool PlayerIsContactWall=false;
    //void OnCollisionStay2D(Collider2D other)
    //{
    //    Debug.Log("ss");
    //    if (other.tag == "Wall") PlayerIsContactWall = true;
    //}

    /* if (Input.GetMouseButtonDown(0))
        {
            playerPos = this.transform.position;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        if (Input.GetMouseButton(0))
        {
            Vector3 prePos = this.transform.position;
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePos;

            // タッチ入力をサポートしてるかどうからしい
            if (Input.touchSupported)
            {
                diff = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) - mousePos;
            }

            diff.z = 0.0f;
            this.transform.position = playerPos + diff * 0.75f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            playerPos = Vector3.zero;
            mousePos = Vector3.zero;
        }
    }*/
}