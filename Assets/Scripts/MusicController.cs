using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip DefaultClip;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceSFX;
    [SerializeField] AudioSource ReserveaudioSourceSFX;

    public static MusicController instance;

    public AudioMixer mainMixer;
    public AudioMixer SoundMixer;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("SavedMusicVolume"))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat("SavedMusicVolume");
            mainMixer.SetFloat("MusicVolume", savedMusicVolume);
        }
        else
        {
            mainMixer.SetFloat("MusicVolume", 0);
        }

        if (PlayerPrefs.HasKey("SavedSoundVolume"))
        {
            float savedSoundVolume = PlayerPrefs.GetFloat("SavedSoundVolume");
            mainMixer.SetFloat("SoundsVolume", savedSoundVolume);
        }
        else
        {
            mainMixer.SetFloat("SoundsVolume", 0);
        }



        PlayerPrefs.Save();

    }


    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

    }

    internal void SetMusicVolume(float value)
    {
        mainMixer.SetFloat("MusicVolume", value);
        PlayerPrefs.SetFloat("SavedMusicVolume", value);
    }

    internal void SetSoundVolume(float value)
    {
        //SoundMixer.SetFloat("SoundMixer", value);
        mainMixer.SetFloat("SoundsVolume", value);
        PlayerPrefs.SetFloat("SavedSoundVolume", value);
    }

    public void SetMainTrack(AudioClip Track)
    {
        audioSource.clip = Track;
        audioSource.Play();
    }

    public void RestartTrack()
    {
        audioSource.Stop();
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSourceSFX.Stop();
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
    }

    public void PlaySFX2(AudioClip clip)
    {
        ReserveaudioSourceSFX.clip = clip;
        ReserveaudioSourceSFX.Play();
    }

    public AudioSource GetMusic()
    {
        return audioSource;
    }

    public AudioSource GetSounds()
    {
        return audioSourceSFX;
    }

}
