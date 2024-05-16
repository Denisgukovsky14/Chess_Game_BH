using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlate : MonoBehaviour
{
    // Звуковые поля
    [SerializeField] private AudioSource ExitSound;
    [SerializeField] private AudioSource StepSound;
    [SerializeField] private AudioSource PlayerDeathSound;
    [SerializeField] private AudioSource NaginateSound;

    private bool IsDeath = false ;
    


    private AudioSource[] audioSources ;


    // Носитель класса Game
    public GameObject controller;
    public float Volume = 1.0f;

    //public GameObject[] HealItems;

    GameObject reference = null;

    public HealPotion[] Potions;


    // Позиции на доске
    int matrixX;
    int matrixY;

    public bool attack = false;



    // При атаке цвет спрайта изменяется
    public void Start()
    {
        //Volume = 0.1f;
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
            
            // Получение координат сущности
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

                // Вектор предполагаемого движения
                Vector2 targetVector = new Vector2(matrixX - 3.5f, matrixY - 3.5f);

                // Получение координат врага на карте
                Vector2 GuardPosition = GameObject.Find("Guardian").transform.position;

                // Получение координат выхода на сцене
                Vector2 Exit = controller.GetComponent<Game>().Exit.transform.position;

                // Если Попытка атаковать стража, или попытка войти в препятствите 

                if (controller.GetComponent<Game>().ObCoords.Contains(targetVector) || targetVector == GuardPosition)
                {
                    controller.GetComponent<Game>().NextTurn();
                    return;
                }

            GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(StepSound.clip);

            // Хилка

            if ( controller.GetComponent<Game>().HealItemsCoords.Contains(targetVector) )
            {
                reference.GetComponent<Chessman>().Heal(10);
                controller.GetComponent<Game>().DeletePotion(targetVector);
            }

            


            // Если Игрок достиг выхода ( В будущем доделать логику с ключом )
            if ( targetVector == Exit)
                    {
                        GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(ExitSound.clip);

                        Destroy(GameObject.Find("Warrior"));
                        controller.GetComponent<Game>().Win();
                        //Destroy(cp);
                    }

                // Проверка на убийство Игрока
                    if (cp != null)
                    {
                        //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(NaginateSound.clip);
                        if (cp.name == "Warrior")
                        {
                            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(NaginateSound.clip);
                            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX2(PlayerDeathSound.clip);
                            //GameObject.Find("Guardian").GetComponent<Chessman>().SetupAtack();
                            controller.GetComponent<Game>().Winner("Стражник убил игрока!");
                            reference.GetComponent<Chessman>().Killed( GameObject.Find("Warrior").GetComponent<Chessman>() );
                            
                            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(PlayerDeathSound.clip);

                }
            }
            
            if (cp == null)
            {
                Destroy(cp);
            }

            }

            GameObject cp2 = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
        if (cp2 == null)
        {

            controller.GetComponent<Game>().SetEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetYBoard());


            //reference.GetComponent<Chessman>().SetXBoard(matrixX);
            //reference.GetComponent<Chessman>().SetYBoard(matrixY);
            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(StepSound.clip);
            reference.GetComponent<Chessman>().SetCoords(matrixX, matrixY);
            //reference.GetComponent<PlayerBladeDamage>().isAttacking = false;

            //reference.GetComponent<Chessman>().StartMove(matrixX, matrixY);
            //reference.GetComponent<Chessman>().SetCoords();

            controller.GetComponent<Game>().SetPosition(reference);

            Debug.Log(controller.GetComponent<Game>().GetCurrentPlayer());

            controller.GetComponent<Game>().NextTurn();


            reference.GetComponent<Chessman>().DestroyMovePlates();
        }

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

    public void SetVolume(float volume)
    {
        Volume = volume;
    }

    public AudioSource[] GetSound()
    {
        audioSources = new AudioSource[3] { ExitSound, StepSound, PlayerDeathSound };
        return audioSources;
    }

    private void Update()
    {
        //Volume = GameObject.Find("SoundsContainer").GetComponent<NoiseController>().Volume;
    }

}
