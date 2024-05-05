using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealScript : MonoBehaviour
{
    // Здоровье и максимум здоровья

    public float health = 10.0f;
    public float maxHealth = 100.0f;
    public Vector2 PotionPosition;

    public void Start()
    {
        PotionPosition = new Vector2(transform.position.x, transform.position.y);
    }

    public void Heal(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log("hill");
    }

}
