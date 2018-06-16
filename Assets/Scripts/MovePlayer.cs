using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの移動処理
// ※マウスの移動にたいして動くだけ
public class MovePlayer : MonoBehaviour {

    private Vector3 playerPos;
    private Vector3 mousePos;

    void Update()
    {
        PlayerControl();
    }
    private void PlayerControl()
    {
        if (Input.GetMouseButtonDown(0))
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
    }
}