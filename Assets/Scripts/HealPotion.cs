using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public float healAmount = 10.0f;
    public float maxHealth = 100.0f;
    public Vector2 PotionPosition;
    [SerializeField] GameObject Warrior;

    public void Start()
    {
        PotionPosition = new Vector2(transform.position.x, transform.position.y);
    }

    public void Heal()
    {
        Chessman player = Warrior.GetComponent<Chessman>();
        player.Heal(healAmount);
        Destroy(gameObject);
    }

}
