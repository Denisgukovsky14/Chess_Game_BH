using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioSource AudioSourceSFX;

    private Slider slider;

    private MusicController Manager ;


    void Start()
    {

        Manager = MusicController.instance;
        slider = GetComponent<Slider>();

        if ( this.name == "NoiseVolume")
        {
            if (PlayerPrefs.HasKey("SavedSoundVolume"))
            {
                slider.value = PlayerPrefs.GetFloat("SavedSoundVolume");
            }
        }
        else { 

        if (PlayerPrefs.HasKey("SliderValue"))
        {
            slider.value = PlayerPrefs.GetFloat("SliderValue");
        }
        }

    }

    public void OnSliderValueChanged(float value)
    {
       Manager.SetMusicVolume(value);
    }

    public void OnSliderValueChangedSounds(float value)
    {
        Manager.SetSoundVolume(value);
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("SliderValue", slider.value);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("SliderValue", slider.value);
        PlayerPrefs.Save();
    }

    void Awake()
    {
        if (slider != null && PlayerPrefs.HasKey("SliderValue"))
        {
            slider.value = PlayerPrefs.GetFloat("SliderValue");
        }
    }


}
