using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSlider : MonoBehaviour
{
    public Slider volumeSlider;


    public void OnVolumeChanged()
    {
        //MusicController.instance.SetSoundVolume(volumeSlider.value);


    }
    /*
    //public MovePlate Controller;
    private Slider slider;
    //public float LE;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.SetValueWithoutNotify(MusicManager.VolumeSound);

        slider.onValueChanged.AddListener(OnSliderValueChanged);

        if (PlayerPrefs.HasKey("SliderValue"))
        {
            slider.value = PlayerPrefs.GetFloat("SliderValue");
        }

    }

    public void OnSliderValueChanged(float value)
    {
        //Controller.SetVolume(value) ;
        MusicManager.VolumeSound = value;
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
    }*/

    public NoiseController Container;
    private Slider slider;

    void Start()
    {
        return;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        //audioSource = GameObject.FindObjectOfType<AudioSource>();

        if (PlayerPrefs.HasKey("SliderValue"))
        {
            slider.value = PlayerPrefs.GetFloat("SliderValue");
        }

    }

    public void OnSliderValueChanged(float value)
    {
        return;
        Container.SetVolume(value);
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
