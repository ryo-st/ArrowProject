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

    Color ColorCache1 = Color.Black;
    Color ColorCache2 = Color.White;

    Color ColorSet()
    {
        if (ColorCache1 == ColorCache2) {
            if (ColorCache1 == Color.White)
            {
                ColorCache2 = ColorCache1;
                ColorCache1 = Color.Black;
                return Color.Black;
            }
            else {
                ColorCache2 = ColorCache1;
                ColorCache1 = Color.White;
                return Color.White;
            }
        }
        switch (Random.Range(0, 2))
        {
            case 0:
                ColorCache2 = ColorCache1;
                ColorCache1 = Color.White;
                return Color.White;
            case 1:
                ColorCache2 = ColorCache1;
                ColorCache1 = Color.Black;
                return Color.Black;
            default:
                return Color.White;//Meaningless
        }
        
    }
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


            //Adjust the distance between arrows
            for (int i = 0; i < EnemyNumber; i++)
            {
                if (Enemys[i].GetObj() != null)
                {
                    if (SpawnContactMargin > Vector3.Distance(Enemys[i].GetObj().transform.position, SpawnPosition))
                        IsRespawn = true;
                }
            }
        }
        if (enemy.GetObj() == null)
        {
            enemy.SetObj(Instantiate(PrefabEnemy, SpawnPosition, Quaternion.Euler(0.0f, 0.0f, angle)) as GameObject);

        }
        else
        {
            enemy.GetObj().transform.position = new Vector3(SpawnPosition.x, SpawnPosition.y, 0);
            enemy.GetObj().transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
        //Color Setting
        enemy.color = ColorSet();
        if (enemy.color == Color.White)
            enemy.SetImage(WhiteEnemy);
        else
            enemy.SetImage(BlackEnemy);
    }
    void Start() {
        ////Dummy
        dummyPlayer = new Player();
       ////Dummy

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
                Enemys[i].GetObj().transform.position = Enemys[i].GetObj().transform.position + Enemys[i].Direction * EnemySpeed;
                ep = Enemys[i].GetObj().transform.position;
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

    void Update()
    {

        for (int i = 0; i < EnemyNumber; i++)
        {
            if (Enemys[i].IsRunning)
            {
                switch (Enemys[i].IsContact(dummyPlayer))
                {
                    case PlayerCollisionState.Break:
                        GameEnd = true;
                        Time.timeScale = 0f;
                        break;
                    case PlayerCollisionState.Absorption:
                        Enemys[i].IsRunning = false;
                        SpwanEnemy(Enemys[i]);
                        //Effect
                        Score++;
                        Debug.Log(Score);
                        break;
                    case PlayerCollisionState.none:
                        break;
                }
            }
        }
    }
    public bool GameEnd = false;
    public int Score = 0;

    public Player dummyPlayer;
    public class Player
    {
        public Color color;
        public Player() { color = Color.White; }
    }

    public enum Color { White,Black };

    enum PlayerCollisionState { none, Break, Absorption }
    class Enemy {
        private GameObject Obj;
        public Vector3 Direction;
        public bool IsRunning;
        public Color color;
        private SpriteRenderer SpriteR;
        private CollisionChecker CollisionCheck;
        public Enemy(){ }
        public void SetObj(GameObject p_Obj)
        {
            Obj = p_Obj;
            CollisionCheck = Obj.GetComponent<CollisionChecker>();
            SpriteR = Obj.GetComponent<SpriteRenderer>();
        }
        public GameObject GetObj() { return Obj; }
        public void SetImage(Sprite image)
        {
            SpriteR.sprite = image;
        }
        public PlayerCollisionState IsContact(Player p_Player)
        {
            if (CollisionCheck.IsContact)
            {
                CollisionCheck.IsContact = false;
                if (p_Player.color == this.color)
                {
                    return PlayerCollisionState.Absorption;
                }
                else {return PlayerCollisionState.Break; }
            }
            return PlayerCollisionState.none;
        }
    }
}
