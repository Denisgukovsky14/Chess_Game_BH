using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    
    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        //myFx.PlayOneShot(clickFx);
        GameObject.Find("MainTheme").GetComponent<MusicController>().PlaySFX(clickFx);
    }

    
}
