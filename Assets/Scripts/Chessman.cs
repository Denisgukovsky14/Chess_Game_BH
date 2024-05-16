using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Chessman : MonoBehaviour
{
    //public Enemy Enemy ;

    public float armor_lv1 = 0f;
    public float max_armor;
    private bool IsLeft ;

    public Animator animator;
    //public AnimationExchanger A_Operator;
    public Transform Position;
    public float Speed;

    [SerializeField] private AudioSource HealSound;
    

    public LayerMask obstacleLayer;

    //ссылки
    public GameObject Controller;
    public GameObject movePlate;

    //Жизнь и максимум жизни
    public float health = 10.0f;
    public float maxHealth = 100.0f;
    public int rumble = 0;

    //позиции
    [SerializeField] int xBoard = 0;
    [SerializeField] int yBoard = 0;

    //очередь ходить
    private string player ;

    //ссылка на спрайт
    public Sprite Black_King;
    public Sprite White_King;


    // Вражеские поля и методы

    public Transform Warrior ;
    public float moveSpeed = 5f;
    public float gridSize = 1f;
    private Vector3 targetPosition;

    public void FindWarrior()
    {
        if ( GameObject.Find("Warrior").transform != null )
        {
            Warrior = GameObject.Find("Warrior").transform;
        } 
    }

    // Функция Activate с проверкой вызывателя метода через this.name
    public void Activate()
    {
        animator.GetComponent<Animator>();
        //A_Operator = this.GetComponent<AnimationExchanger>();

        Controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords( xBoard, yBoard );

        switch (this.name)
        {
            case "Guardian":
                this.GetComponent<SpriteRenderer>().sprite = Black_King;
                player = "Enemy";
                break;
            case "Warrior":
                //animator.GetComponent<Animator>();
                this.GetComponent<SpriteRenderer>().sprite = White_King;
                player = "Player";
                break;
        }
    }

    public void Update()
    {
        

        

    }


    // Эта функция изменяет координаты obj из класса Game через this.

    public void SetCoords( int x, int y)
    {

        if( xBoard < x)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            IsLeft = false;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            IsLeft = true;
        }

        xBoard = x;
        yBoard = y;

        float Newx = xBoard;
        float Newy = yBoard;


        Newx += -3.5f;
        Newy += -3.5f;

        StartCoroutine(Move(Newx, Newy));
        

        if (this.GetComponent<PlayerBladeDamage>() != null)
        {
            this.GetComponent<PlayerBladeDamage>().isAttacking = false;
        }
        //this.transform.position = new Vector3(Newx, Newy, 0);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    // Оптимизировать все до одного - двух методов, задавать координаты в МУВ

    private IEnumerator Move(float x, float y)
    {

        if (_isDeath)
        {
            yield break;
        }
        var FinalPos = new Vector2( x, y );
        const float moveTime = 1f;
        var time = 0f;
        State = States.Move;

        while (time < moveTime)
        {
            transform.position = Vector3.Lerp(this.transform.position, FinalPos, time);
            time += Time.deltaTime;

            yield return null;

            if ( time >= 0.5f  )
            {
                State = States.Idle;
            }
            
        }





        //State = States.Idle;


        //xBoard = x;
        //yBoard = y;
        //SetCoords();

    }

    public void StartMove(float x, float y)
    {
        
    }

    public void OnMouseUp()
    {
        if (!Controller.GetComponent<Game>().IsGameOver() && Controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {

            DestroyMovePlates();

            InitiateMovePlates();

        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {

        switch (this.name)
        {
            case "Guardian":
                GuardianMovement();
                //PlayerMovement();
                break;
            case "Warrior":
                PlayerMovement();
                break;   
        }
    }

    public void GuardianMovement()
    {

        // Находим расстояние по горизонтали и вертикали между Guardian и игроком
        float horizontalDistance = GameObject.Find("Guardian").transform.position.x - GameObject.Find("Warrior").transform.position.x;
        float verticalDistance = GameObject.Find("Guardian").transform.position.y - GameObject.Find("Warrior").transform.position.y;

    // Если горизонтальное расстояние больше вертикального, двигаемся по горизонтали

        if (Mathf.Abs(horizontalDistance) > Mathf.Abs(verticalDistance))
    {
        if (horizontalDistance > 0)
        {

            MovePlateAttackSpawn(xBoard - 1, yBoard);
            
            }
        else
        {
            
            MovePlateAttackSpawn(xBoard + 1, yBoard);
            }
    }

    // Иначе двигаемся по вертикали

    else
    {
        if (verticalDistance > 0)
        {
            
            MovePlateAttackSpawn(xBoard, yBoard - 1 );
            }
        else
        {
            
            MovePlateAttackSpawn(xBoard, yBoard + 1);
            }
    }
    }

    public void PlayerMovement()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);

        // Движение по диагонали закомментированно.

        //PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        //PointMovePlate(xBoard - 1, yBoard + 1);

        //PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        //PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
  
        Game sc = Controller.GetComponent<Game>();
        
        if ( sc.PositionOnBoard( x, y ))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null )
            {
                MovePlateAttackSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void MovePlateAttackSpawn( int matrixX, int matrixY )
    {
        float x = matrixX;
        float y = matrixY;

        x -= 3.5f;
        y -= 3.5f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, 0), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();

        mpScript.attack = true;

        mpScript.SetReference(gameObject);

        mpScript.SetCoords(matrixX, matrixY);
        
        
        if ( this.name == "Guardian")
        {
            mpScript.OnMouseUp();
            if ( GameObject.Find("Guardian").transform.position == GameObject.Find("Warrior").transform.position)
            {
                Destroy(GameObject.Find("Warrior"));
            }
        }
    }



    public void Heal(float amount)
    {
        HealSound.Play();
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log("healed");
    }


    public void SetupAtack()
    {
        State = States.Atack;
   
    }

    public void Killed( Chessman Victim )
    {
        Victim.Death();
    }


    public void ToIdle()
    {
        State = States.Idle;
    }

    private bool _isDeath;

    public void Death()
    {
        State = States.Death;
        _isDeath = true;
        Debug.Log(State);
    }

    private States _currentState; 

    private States State
    {
        get {
            return _currentState;
            //return (States)animator.GetInteger("State"); 
        }
        set {

            if (_isDeath)
            {
                return;
            }

            if (_currentState == value)
            {
                return;
            }

            //Debug.LogError($"SET STATE {gameObject.name} OLD STATE = {(States)animator.GetInteger("State")} NEW STATE =  {value}");

            _currentState = value;
            animator.SetInteger("State", (int)value);
            //animator.SetTrigger("Attack");
        }
    }

    public void Damage(float damage_1)
        {
            if ((armor_lv1 > 0))
            {
                health -= (damage_1 / 2);
                armor_lv1 -= 1f;
            }

            if (armor_lv1 <= 0)
            {
                health -= damage_1;
            }

            if (health <= 0f)
            {
                Controller.GetComponent<Game>().Winner("Стражник убил игрока!");
                Death();
            }
        }

    public void Heal_armor(float heal_armor)
    {

        armor_lv1 += heal_armor;
        if (armor_lv1 >= max_armor)
        {
            armor_lv1 = max_armor;
        }

    }



    /*
    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }
    */

}

public enum States
{
    Idle = 0,
    Move = 1,
    Atack = 2,
    Death = 3
}
