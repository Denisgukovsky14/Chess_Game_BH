using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_hp : MonoBehaviour
{
    public float hp = 10.0f;


    public void Damage(float damage_1)
    {
        hp -= damage_1;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
