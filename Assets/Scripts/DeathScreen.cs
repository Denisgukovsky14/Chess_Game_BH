using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject DeathPanel;

    private void Awake()
    {
        DeathPanel.SetActive(false);
    }

    public void SetDeathScreen()
    {
        DeathPanel.SetActive(true);
    }

    public void OffDeathScereen()
    {
        DeathPanel.SetActive(false);

    }
}
