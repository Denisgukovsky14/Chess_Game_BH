using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experiment : MonoBehaviour
{
    public AudioSource audioSource;
    private Slider slider;

    void Start()
    {
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
        audioSource.volume = value;
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
