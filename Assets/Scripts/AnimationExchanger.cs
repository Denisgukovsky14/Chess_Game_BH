using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExchanger : MonoBehaviour
{

    private Animator animator;
    private Chessman CurrentEntity;

    public void Awake()
    {
        CurrentEntity = this.GetComponent<Chessman>();
        animator = this.GetComponent<Animator>();
    }

    public void Update()
    {
        State = States.Idle;
    }

    public void Move()
    {
        State = States.Move;
    }

    public void Atack()
    {
        State = States.Atack;     
    }


    public void Death()
    {
        State = States.Death;
    }

    private States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    
    public enum States
    {
        Idle = 0,
        Move = 1,
        Atack = 2,
        Death = 3
    }

}
