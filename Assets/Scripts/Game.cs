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

    // ����������� ������
    public GameObject Simple_Figure;

    // ������ ������� �� ����
    private GameObject[,] positions = new GameObject[8, 8];

    // ������ 
    private GameObject[] Players = new GameObject[2];

    // ������ ������ �����
    private GameObject[] Enemies = new GameObject[5];

    // ������ ��������� ����������� �� ������
    public List<Vector2> ObCoords;

    //����� � ������ ( ������� ������ )

    public GameObject Exit;

    // ������� �����
    private string currentPlayer = "Enemy";

    // ���������� ���������� �� ����
    private bool gameOver = false;

    void Start()
    {
        //mainCamera.enabled = true;
        cameraController = Camera.main.GetComponent<CamerController>();

        // �������� ������� ��������� �����������
        List<GameObject> Obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        ObCoords = new List<Vector2>(NumerCoords(Obstacles));

        // �������� ����� ����������� �������� Create()
        Players = new GameObject[]
        {
            Create("Warrior",4,0)
        };

        
        Enemies = new GameObject[]
        {
            Create("Guardian",4,7)
        };


        // ������������ ����� ����������� �������� SetPosition()
        for (int i = 0; i < Enemies.Length; i++)
        {
            SetPosition(Enemies[i]);
            SetPosition(Players[i]);
        }


        Debug.Log("��� ��� �������� ����������� � Game");
        cameraController.enabled = true;
    }

    // ���� ������� Create()
    public GameObject Create(string name, int x, int y)
    {
        // �������� ������ ����������, ��������� � ������� ���������� 
        GameObject obj = Instantiate(Simple_Figure, new Vector3(0, 0, 0), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();

        cm.name = name;

        cm.SetXBoard(x);
        cm.SetYBoard(y);

        cm.Activate();

        // ��� ������ �����, �� ���, ����� ������ ������ ���
        if (cm.name == "Guardian" && currentPlayer == "Enemy")
        {
            GameObject.Find("Guardian").GetComponent<Chessman>().OnMouseUp();
        }

        //cameraController.enabled = true;

        // ����������� �������
        return obj;


    }

    // ���� ������� SetPosition()
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
        TextComponent.text = playerWinner + "\n\n" + "������� �� �����, ����� ������ ������" ;
    }

    public List<Vector2> NumerCoords(List<GameObject> Obstacles)
    {

        foreach (GameObject obj in Obstacles)
        {
            Vector2 objPosition = obj.transform.position; // ��������� ������� �������

            ObCoords.Add(objPosition);
        }

        return ObCoords;
    }

}
