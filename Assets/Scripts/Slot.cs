using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int number_slot;
    public float healAmount = 2f;
    public int damageUp = 1;
    public float teleport = 10.0f;

    public Chessman player;
    public PlayerBladeDamage swordAttack;

    private Inventory invent;

    private void Start()
    {
        GameObject ChessManObject = GameObject.FindGameObjectWithTag("Player");
        if (ChessManObject != null)
        {
            player = ChessManObject.GetComponent<Chessman>();

            swordAttack = ChessManObject.GetComponent<PlayerBladeDamage>();

            invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            invent.isFull[number_slot] = false;
        }
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void UseItem()
    {
        if (transform.childCount > 0) // Проверяем, есть ли предмет в слоте
        {
            Transform item = transform.GetChild(0); // Получаем первый дочерний объект (предмет)

            if (item.CompareTag("Hill_armor")) // Проверяем, является ли предмет хилом (предметом восстановления здоровья)
            {
                if (player != null) // Проверяем, что переменная player инициализирована
                {
                    player.Heal_armor(healAmount);
                    Destroy(item.gameObject);
                }

            }

            if (item.CompareTag("damageUp"))
            {
                if (swordAttack != null)
                {
                    swordAttack.BoostDamage( damageUp );
                    Destroy(item.gameObject);
                }
            }
        }
    }

}
