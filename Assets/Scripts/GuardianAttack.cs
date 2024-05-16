using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAttack : MonoBehaviour
{
    [SerializeField] AudioClip AtackSound;
    [SerializeField] AudioClip Death;
    public float damage_1 = 10.0f;

    private Collider2D CurrentEnemy;
    private bool AtackPermission = false;


    public void OnAttackButtonPressed()
    {
        if (AtackPermission == true )
        {
            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(AtackSound);
            DealingDamage(CurrentEnemy);
            AtackPermission = false;

        }
    }



    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.name == "Warrior")
        {
            GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(AtackSound);
            //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(Death);
            CurrentEnemy = collision;
            AtackPermission = true;
            OnAttackButtonPressed();

        }

    }



    public void DealingDamage(Collider2D collision)
    {

        this.GetComponent<Chessman>().SetupAtack();
        //GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(AtackSound);

        CurrentEnemy.GetComponent<Chessman>().Damage(damage_1);

    }
}
