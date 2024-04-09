using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 pos;

    private void OnEnable()
    {
        Debug.Log("Вот это действие выполняется в CamerC");

        if (!player)
            player = GameObject.Find("Warrior").transform;
        
    }

    private void Update()
    {
        if (player != null)
        {

            pos = player.position;
            pos.z = -10f;
            pos.y += 3f;

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }
    }

}