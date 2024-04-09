using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    // Носитель класса Game
    public GameObject controller;
    GameObject reference = null;

    // Позиции на доске
    int matrixX;
    int matrixY;

    public bool attack = false;

    // При атаке цвет спрайта изменяется
    public void Start()
    {
        /*
        if (attack)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        */
    }

    // Обработка Клика
    public void OnMouseUp()
    {
        // инициализация контрллера
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Если атакует
            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
                Vector2 targetVector = new Vector2(matrixX - 3.5f, matrixY - 3.5f);
                Vector2 GuardPosition = GameObject.Find("Guardian").transform.position;
                Vector2 Exit = controller.GetComponent<Game>().Exit.transform.position;

                // Если Попытка атаковать стража, или попытка войти в препятствите 

                if (controller.GetComponent<Game>().ObCoords.Contains(targetVector) || targetVector == GuardPosition)
                {
                    controller.GetComponent<Game>().NextTurn();
                    return;
                }

                // Если Игрок достиг выхода ( В будущем доделать логику с ключом )
                if ( targetVector == Exit)
                {
                    Destroy(GameObject.Find("Warrior"));
                    controller.GetComponent<Game>().Winner("Вам удалось сбежать от стража!");
                    //Destroy(cp);
                }

                // Проверка на убийство Игрока
                if (cp != null)
                {
                    if (cp.name == "Warrior")
                    {
                        controller.GetComponent<Game>().Winner("Стражник убил игрока!");
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
        

        // Метод, благодаря которому враг ходит сам.
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
