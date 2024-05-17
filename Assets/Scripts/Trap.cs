using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool TrapWorked = true ;
    public AudioClip TrapSound;
    public float damage = 5.0f;
    public float displayTime = 1.0f; // Время отображения спрайта

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Спрайт изначально невидим
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Warrior")
        {

            StartCoroutine(DisplayTrap());
            GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(TrapSound);
            Chessman player = collision.GetComponent<Chessman>();

            if (TrapWorked)
            {
                player.Damage(damage);
                TrapWorked = false;
            }
            //Destroy(gameObject);
            Destroy(gameObject, displayTime + 0.1f);

        }
    }

    IEnumerator DisplayTrap()
    {
        spriteRenderer.enabled = true; // Делаем спрайт видимым
        yield return new WaitForSeconds(displayTime); // Ждем указанное время
        spriteRenderer.enabled = false; // Спрайт исчезает
    }

}
