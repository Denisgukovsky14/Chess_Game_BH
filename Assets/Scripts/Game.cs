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

    // ������ ��������� ����� �� ������
    public List<Vector2> HealItemsCoords;

    public List<GameObject> Potions;
    public List<GameObject> HealItems;

    //����� � ������ ( ������� ������ )

    public GameObject Exit;

    // ������� �����
    private string currentPlayer = "Enemy";

    // ���������� ���������� �� ����
    private bool gameOver = false;

    void Start()
    {
        //GameObject.Find("MainTheme").GetComponent<MusicController>().RestartTrack();
        GameObject.Find("Guardian").GetComponent<Chessman>().Activate();
        GameObject.Find("Warrior").GetComponent<Chessman>().Activate();

        if (currentPlayer == "Enemy")
        {
            Debug.Log("����������");
            GameObject.Find("Guardian").GetComponent<Chessman>().OnMouseUp();
        }

        //mainCamera.enabled = true;
        cameraController = Camera.main.GetComponent<CamerController>();

        // �������� ������� ��������� �����������
        List<GameObject> Obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
        ObCoords = new List<Vector2>(NumerCoords(Obstacles));

        // �������� ������� ��������� �������
        HealItems = new List<GameObject>(GameObject.FindGameObjectsWithTag("Heal"));
        HealItemsCoords = new List<Vector2>(NumerHealCoords(HealItems));

        // �������� ����� ����������� �������� Create()
        Players = new GameObject[]
        {
            //Create("Warrior",5,0),
            GameObject.Find("Warrior")
        };

        
        Enemies = new GameObject[]
        {
            //Create("Guardian",4,7)
            GameObject.Find("Guardian")
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


    /*
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
    */

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

            GameObject.Find("MainTheme").GetComponent<MusicController>().RestartTrack();
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

    public List<Vector2> NumerHealCoords(List<GameObject> HealItems)
    {

        foreach (GameObject obj in HealItems)
        {
            Vector2 objPosition = obj.transform.position; // ��������� ������� �������

            HealItemsCoords.Add(objPosition);
        }

        return HealItemsCoords;
    }

    public void DeletePotion( Vector2 PotionCoord )
    {
        int index = HealItemsCoords.FindIndex(element => element == PotionCoord);
        Destroy(HealItems[index]);
        HealItems.Remove(HealItems[index]);
    }

}
