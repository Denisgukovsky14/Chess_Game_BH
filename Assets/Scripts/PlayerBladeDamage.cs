using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBladeDamage : MonoBehaviour
{
    [SerializeField] AudioClip AtackSound;
    public float damage_1 = 1.0f;
    public bool isAttacking = false;

    private bool IsBoost = false;
    private int Boost;

    private Collider2D CurrentEnemy;
    private bool AtackPermission = false;


    public Button attackButton;

    private void Start()
    {
        attackButton.interactable = false;
        //attackButton.onClick.AddListener(OnAttackButtonPressed);
        //Debug.Log("Я нажался");
        //attackButton.onClick.AddListener(OnAttackButtonPressed);
        //attackButton.onClick.AddListener(OnAttackButtonPressed);
        //attackButton.onClick.AddListener(OnAttackButtonPressed);

    }

    private void Update()
    {

        //this.GetComponent<Chessman>().ToIdle();

    }


    public void OnAttackButtonPressed()
    {
        //isAttacking = true;
        if ( AtackPermission == true && isAttacking == false  )
        {


            DealingDamage(CurrentEnemy);
            AtackPermission = false;
            isAttacking = true;
            attackButton.interactable = false;


            //this.GetComponent<Chessman>().Controller.GetComponent<Game>().NextTurn();

        }
    }

    public void BoostDamage( int boost )
    {
        IsBoost = true;
        Boost = boost;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        attackButton.interactable = true;
        /*
        if ( isAttacking == true && collision.name == "Guardian" )
        {
            DealingDamage(collision);
            isAttacking = false;
        }
        */

        //isAttacking = (collision.name == "Guardian") ? true : false;

        

        if ( collision.name == "Guardian" )
        {
            
            CurrentEnemy = collision;
            AtackPermission = true;
           
        }

    }



    public void DealingDamage(Collider2D collision)
    {

        this.GetComponent<Chessman>().SetupAtack();
        //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX2(PlayerDeathSound.clip);
        GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(AtackSound);
        Mob_hp mob_hp = collision.GetComponent<Mob_hp>();

        if (IsBoost)
        {
            mob_hp.Damage(damage_1 + Boost);
            IsBoost = false;
        }
        else
        {
            mob_hp.Damage(damage_1);
        }

    }




}
