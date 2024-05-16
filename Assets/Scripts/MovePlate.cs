using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlate : MonoBehaviour
{
    // �������� ����
    [SerializeField] private AudioSource ExitSound;
    [SerializeField] private AudioSource StepSound;
    [SerializeField] private AudioSource PlayerDeathSound;
    [SerializeField] private AudioSource NaginateSound;

    private bool IsDeath = false ;
    


    private AudioSource[] audioSources ;


    // �������� ������ Game
    public GameObject controller;
    public float Volume = 1.0f;

    //public GameObject[] HealItems;

    GameObject reference = null;

    public HealPotion[] Potions;


    // ������� �� �����
    int matrixX;
    int matrixY;

    public bool attack = false;



    // ��� ����� ���� ������� ����������
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


    // ��������� �����
    public void OnMouseUp()
    {
        // ������������� ����������
        controller = GameObject.FindGameObjectWithTag("GameController");


        // ���� �������
            if (attack)
            {
            
            // ��������� ��������� ��������
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

                // ������ ��������������� ��������
                Vector2 targetVector = new Vector2(matrixX - 3.5f, matrixY - 3.5f);

                // ��������� ��������� ����� �� �����
                Vector2 GuardPosition = GameObject.Find("Guardian").transform.position;

                // ��������� ��������� ������ �� �����
                Vector2 Exit = controller.GetComponent<Game>().Exit.transform.position;

                // ���� ������� ��������� ������, ��� ������� ����� � ������������ 

                if (controller.GetComponent<Game>().ObCoords.Contains(targetVector) || targetVector == GuardPosition)
                {
                    controller.GetComponent<Game>().NextTurn();
                    return;
                }

            GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(StepSound.clip);

            // �����

            if ( controller.GetComponent<Game>().HealItemsCoords.Contains(targetVector) )
            {
                reference.GetComponent<Chessman>().Heal(10);
                controller.GetComponent<Game>().DeletePotion(targetVector);
            }

            


            // ���� ����� ������ ������ ( � ������� �������� ������ � ������ )
            if ( targetVector == Exit)
                    {
                        GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(ExitSound.clip);

                        Destroy(GameObject.Find("Warrior"));
                        controller.GetComponent<Game>().Win();
                        //Destroy(cp);
                    }

                // �������� �� �������� ������
                    if (cp != null)
                    {
                        //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(NaginateSound.clip);
                        if (cp.name == "Warrior")
                        {
                            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(NaginateSound.clip);
                            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX2(PlayerDeathSound.clip);
                            //GameObject.Find("Guardian").GetComponent<Chessman>().SetupAtack();
                            controller.GetComponent<Game>().Winner("�������� ���� ������!");
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
