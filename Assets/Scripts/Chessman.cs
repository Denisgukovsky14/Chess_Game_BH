using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //public Enemy Enemy ;

    public LayerMask obstacleLayer;

    //ссылки
    public GameObject Controller;
    public GameObject movePlate;

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

        Controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "Guardian":
                this.GetComponent<SpriteRenderer>().sprite = Black_King;
                player = "Enemy";
                break;
            case "Warrior":
                this.GetComponent<SpriteRenderer>().sprite = White_King;
                player = "Player";
                break;
        }
    }


    // Эта функция изменяет координаты obj из класса Game через this.
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;


        x += -3.5f;
        y += -3.5f;

        this.transform.position = new Vector3(x, y, 0);
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
}
