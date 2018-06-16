using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    const int EnemyNumber = 5;
    const int SimultaneousEnemyNumber = 5;
    Enemy[] Enemys = new Enemy[EnemyNumber];

    public GameObject PrefabEnemy;
    Sprite WhiteEnemy, BlackEnemy;

    Vector2 LeftStartPoint, BottomStartPoint;
    Vector2 LeftEndPoint, BottomEndPoint;

    Vector2 RightStartPoint, TopStartPoint;
    Vector2 RightEndPoint, TopEndPoint;

    Vector2 TopRightPoint;

    float SpawnVerticalLength, SpawnHorizontallLength;

    float SpawnAreaRatio = 0.8f;
    float ScreenMargin = 2;
    float SpawnContactMargin = 1;


    void SpwanEnemy(Enemy enemy)
    {
        Vector2 SpawnPosition = Vector2.zero;
        float angle = 0;

        bool IsRespawn = true;
        while (IsRespawn)
        {
            IsRespawn = false;

            Vector2 EnemyDirection = new Vector2(Random.Range(BottomStartPoint.x, BottomEndPoint.x), Random.Range(LeftStartPoint.y, LeftEndPoint.y));

            switch (Random.Range(0, 4))
            {
                case 0:
                    SpawnPosition = new Vector2(LeftStartPoint.x - ScreenMargin, Random.Range(LeftStartPoint.y, LeftEndPoint.y));
                    break;
                case 1:
                    SpawnPosition = new Vector2(RightStartPoint.x + ScreenMargin, Random.Range(RightStartPoint.y, RightEndPoint.y));
                    break;
                case 2:
                    SpawnPosition = new Vector2(Random.Range(TopStartPoint.x, TopEndPoint.x), TopStartPoint.y + ScreenMargin);
                    break;
                case 3:
                    SpawnPosition = new Vector2(Random.Range(BottomStartPoint.x, BottomEndPoint.x), BottomStartPoint.y - ScreenMargin);
                    break;
                default:
                    break;

            }
            var vec = (SpawnPosition - EnemyDirection).normalized;
            enemy.Direction = new Vector3(-vec.x,-vec.y,0);
            angle = (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) - 180;

            for (int i = 0; i < EnemyNumber; i++)
            {
                if (Enemys[i].Obj != null)
                {
                    if (SpawnContactMargin > Vector3.Distance(Enemys[i].Obj.transform.position, SpawnPosition))
                        IsRespawn = true;
                }
            }
        }
        if (enemy.Obj == null)
        {
            enemy.Obj = Instantiate(PrefabEnemy, SpawnPosition, Quaternion.Euler(0.0f, 0.0f, angle)) as GameObject;
        }
        else
        {
            enemy.Obj.transform.position = new Vector3(SpawnPosition.x, SpawnPosition.y, 0);
            enemy.Obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
    }
    void Start() {
        Sprite[] Characters = Resources.LoadAll<Sprite>("chr_256");
   
        WhiteEnemy = Characters[1];
        BlackEnemy = Characters[2];

        Camera camera = Camera.main;
        Vector2 TopLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 BottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

        TopRightPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float VerticalLength = Mathf.Abs(TopLeft.y) + Mathf.Abs(BottomRight.y);
        float HorizontallLength = Mathf.Abs(TopLeft.x) + Mathf.Abs(BottomRight.x);
        SpawnVerticalLength = (VerticalLength) * SpawnAreaRatio;
        SpawnHorizontallLength = (HorizontallLength) * SpawnAreaRatio;

        LeftStartPoint = new Vector2(TopLeft.x, TopLeft.y - VerticalLength * (1.0f - SpawnAreaRatio) / 2.0f);
        BottomStartPoint = new Vector2(BottomRight.x - HorizontallLength * (1.0f - SpawnAreaRatio) / 2.0f, BottomRight.y);

        LeftEndPoint = new Vector2(LeftStartPoint.x, LeftStartPoint.y - SpawnVerticalLength);
        BottomEndPoint = new Vector2(BottomStartPoint.x - SpawnHorizontallLength, BottomStartPoint.y);

        RightStartPoint = new Vector2(TopRightPoint.x, LeftStartPoint.y);
        RightEndPoint = new Vector2(TopRightPoint.x, LeftEndPoint.y);

        TopStartPoint = new Vector2(BottomStartPoint.x, TopRightPoint.y);
        TopEndPoint = new Vector2(BottomEndPoint.x, TopRightPoint.y);

        for (int i = 0; i < EnemyNumber; i++)
        {
            Enemys[i] = new Enemy();
        }
        for (int i = 0; i < EnemyNumber; i++)
        {
            SpwanEnemy(Enemys[i]);
        }
        for (int i = 0; i < InitialRunnningNumber; i++)
        {
            Enemys[i].IsRunning = true;
        }
    }
    public int InitialRunnningNumber=3;
    public float EnemySpeed=0.1f;
    public int SpwanRatio = 30;
    void FixedUpdate() {
        int RunningEnemyNumber = 0;
        Vector3 ep;//EnemyPosition
        for (int i = 0; i < EnemyNumber; i++)
        {
            if (Enemys[i].IsRunning)
            {
                Enemys[i].Obj.transform.position = Enemys[i].Obj.transform.position + Enemys[i].Direction * EnemySpeed;
                ep = Enemys[i].Obj.transform.position;
                if ((Enemys[i].Direction.x > 0 && ep.x > RightStartPoint.x + ScreenMargin) ||
                    (Enemys[i].Direction.x < 0 && ep.x < LeftStartPoint.x - ScreenMargin) ||
                    (Enemys[i].Direction.y > 0 && ep.y > TopStartPoint.y + ScreenMargin) ||
                    (Enemys[i].Direction.y < 0 && ep.y < BottomStartPoint.y - ScreenMargin)
                    )
                {
                    Enemys[i].IsRunning = false;
                    SpwanEnemy(Enemys[i]);
                }
                
            }
        }

        for (int i = 0; i < EnemyNumber; i++)
        {
            if (Enemys[i].IsRunning) RunningEnemyNumber++;
        }
        if (RunningEnemyNumber < SimultaneousEnemyNumber && Random.Range(0,100) < SpwanRatio)
        {
            for (int i = 0; i < EnemyNumber; i++)
            {
                if (!Enemys[i].IsRunning) { Enemys[i].IsRunning = true;break; }
            }
        }
    }

    class Enemy {
        public GameObject Obj;
        public Vector3 Direction;
        public bool IsRunning;
        public Enemy(){
        }
    }
}
