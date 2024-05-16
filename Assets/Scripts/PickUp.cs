using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject slotButton;

    private Inventory invent;

    private void Start()
    {
        invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void AddItem()
    {
        //if (Input.GetButtonDown("Button_damage"))
        //{
        for (int number_slot = 0; number_slot < invent.slots.Length; number_slot++)
        {
            if (invent.isFull[number_slot] == false)
            {
                invent.isFull[number_slot] = true;
                Instantiate(slotButton, invent.slots[number_slot].transform);
                //Destroy(gameObject);
                break;
            }
        }
        //}
    }
}
