using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    //public GameObject PrefabTransItem;
    public GameObject TransItem;
    Item TransItemS;

    MovePlayer MovePlayerS;

    int SpwanRatio = 30;
    Vector2 SpawnPosition;
    void Start()
    {
        MovePlayerS = GameObject.Find("player").GetComponent<MovePlayer>();
        TransItemS = TransItem.GetComponent<Item>();

        StartCoroutine("ItemSpawn");
    }
    float rotateSpeed = 15.0f;
    private IEnumerator ItemSpawn()
    {
        while (!MoveEnemy.GameEnd)
        {
            yield return new WaitForSeconds(2f);
            
            if (!TransItemS.IsCoolTime && !TransItemS.IsSetting && Random.Range(0, 100) < SpwanRatio)
            {
                Vector2 TopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
                Vector2 BottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
                SpawnPosition = new Vector2(Random.Range(MoveEnemy.TopStartPoint.x, MoveEnemy.TopEndPoint.x), Random.Range(MoveEnemy.RightStartPoint.y, MoveEnemy.RightEndPoint.y));
                //if (TransItem == null)
                //{
                //    TransItem = Instantiate(PrefabTransItem, SpawnPosition, Quaternion.identity) as GameObject;
                //    TransItemS = TransItem.GetComponent<Item>();
                //}
                //else
                //{
                    TransItem.transform.position = new Vector3(SpawnPosition.x, SpawnPosition.y, 0);
                    TransItem.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                //}
                Debug.Log("distance"+Vector3.Distance(TransItem.transform.position,MovePlayerS.player.Obj.transform.position));
                if((Vector3.Distance(TransItem.transform.position, MovePlayerS.player.Obj.transform.position))>2f)
                    TransItemS.ItemInitialize();
            }
        }
    }

    private void FixedUpdate()
    {
        if (TransItemS != null && TransItemS.IsContact) {
            TransItemS.IsContact = false;
            TransItemS.IsSetting = false;
            MovePlayerS.ChangeColor();
        }
    }
}
