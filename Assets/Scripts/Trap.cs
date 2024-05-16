using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 3.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Warrior")
        {
            Chessman player = collision.GetComponent<Chessman>();
            player.Damage(damage);
            Destroy(gameObject);
        }
    }
}
