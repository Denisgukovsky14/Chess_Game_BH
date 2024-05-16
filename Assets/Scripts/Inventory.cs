using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;

    private bool inventoryOn;

    private void Start()
    {
        inventoryOn = false;
    }

    public void Chest()
    {
        if (inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }

        else
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
    }
}
