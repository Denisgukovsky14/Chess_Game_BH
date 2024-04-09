using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{

    public Camera mainCamera;

    private CamerController cameraController;

    // Абстрактная фигура
    public GameObject Simple_Figure;

    // Массив позиций на поле
    private GameObject[,] positions = new GameObject[8, 8];

    // Массив 
    private GameObject[] Players = new GameObject[2];

    // Массив чёрных фигур
    private GameObject[] Enemies = new GameObject[5];

    // Массив координат препятствий на уровне
    public List<Vector2> ObCoords;

    //Выход с уровня ( условие победы )

    public GameObject Exit;

    // Текущий игрок
    private string currentPlayer = "Enemy";

    // Переменная отвечающая за игру
    private bool gameOver = false;

    void Start()
    {
        //mainCamera.enabled = true;
        cameraController = Camera.main.GetComponent<CamerController>();

        // Создание массива координат препятствий
        List<GameObject> Obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        ObCoords = new List<Vector2>(NumerCoords(Obstacles));

        // Создание фигур специальной функцией Create()
        Players = new GameObject[]
        {
            Create("Warrior",4,0)
        };

        
        Enemies = new GameObject[]
        {
            Create("Guardian",4,7)
        };


        // Расположение фигур специальной функцией SetPosition()
        for (int i = 0; i < Enemies.Length; i++)
        {
            SetPosition(Enemies[i]);
            SetPosition(Players[i]);
        }


        Debug.Log("Вот это действие выполняется в Game");
        cameraController.enabled = true;
    }

    // Сама функция Create()
    public GameObject Create(string name, int x, int y)
    {
        // Создание пустой абстракции, наделение её нужными свойствами 
        GameObject obj = Instantiate(Simple_Figure, new Vector3(0, 0, 0), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();

        cm.name = name;

        cm.SetXBoard(x);
        cm.SetYBoard(y);

        cm.Activate();

        // При спавне врага, он сам, сразу делает первый ход
        if (cm.name == "Guardian" && currentPlayer == "Enemy")
        {
            GameObject.Find("Guardian").GetComponent<Chessman>().OnMouseUp();
        }

        //cameraController.enabled = true;

        // Возвращение объекта
        return obj;


    }

    // Сама функция SetPosition()
    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetEmpty( int x, int y )
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if ( currentPlayer == "Player")
        {
            currentPlayer = "Enemy";
        }
        else
        {
            currentPlayer = "Player";
        }
    }

    public void Update()
    {
        if ( gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;

        GameObject TextBlock = GameObject.FindGameObjectWithTag("WinnerText");
        TextMeshProUGUI TextComponent = TextBlock.GetComponent<TextMeshProUGUI>();
        TextComponent.text = playerWinner + "\n\n" + "Нажмите на экран, чтобы начать заново" ;
    }

    public List<Vector2> NumerCoords(List<GameObject> Obstacles)
    {

        foreach (GameObject obj in Obstacles)
        {
            Vector2 objPosition = obj.transform.position; // получение позиции объекта

            ObCoords.Add(objPosition);
        }

        return ObCoords;
    }

}
