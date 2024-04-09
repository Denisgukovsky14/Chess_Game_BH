using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    // �������� ������ Game
    public GameObject controller;
    GameObject reference = null;

    // ������� �� �����
    int matrixX;
    int matrixY;

    public bool attack = false;

    // ��� ����� ���� ������� ����������
    public void Start()
    {
        /*
        if (attack)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        */
    }

    // ��������� �����
    public void OnMouseUp()
    {
        // ������������� ����������
        controller = GameObject.FindGameObjectWithTag("GameController");

        // ���� �������
            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
                Vector2 targetVector = new Vector2(matrixX - 3.5f, matrixY - 3.5f);
                Vector2 GuardPosition = GameObject.Find("Guardian").transform.position;
                Vector2 Exit = controller.GetComponent<Game>().Exit.transform.position;

                // ���� ������� ��������� ������, ��� ������� ����� � ������������ 

                if (controller.GetComponent<Game>().ObCoords.Contains(targetVector) || targetVector == GuardPosition)
                {
                    controller.GetComponent<Game>().NextTurn();
                    return;
                }

                // ���� ����� ������ ������ ( � ������� �������� ������ � ������ )
                if ( targetVector == Exit)
                {
                    Destroy(GameObject.Find("Warrior"));
                    controller.GetComponent<Game>().Winner("��� ������� ������� �� ������!");
                    //Destroy(cp);
                }

                // �������� �� �������� ������
                if (cp != null)
                {
                    if (cp.name == "Warrior")
                    {
                        controller.GetComponent<Game>().Winner("�������� ���� ������!");
                    }
                }

                Destroy(cp);

            }

        
            controller.GetComponent<Game>().SetEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());

            reference.GetComponent<Chessman>().SetXBoard(matrixX);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);

            reference.GetComponent<Chessman>().SetCoords();

            controller.GetComponent<Game>().SetPosition(reference);

            Debug.Log(controller.GetComponent<Game>().GetCurrentPlayer());

            controller.GetComponent<Game>().NextTurn();

            reference.GetComponent<Chessman>().DestroyMovePlates();
        

        // �����, ��������� �������� ���� ����� ���.
        if ( controller.GetComponent<Game>().GetPosition(matrixX, matrixY) == GameObject.Find("Warrior"))
        {
            GameObject.Find("Guardian").GetComponent<Chessman>().OnMouseUp();
        }

    }

    public void SetCoords( int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
