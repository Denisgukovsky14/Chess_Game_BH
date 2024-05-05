using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioClip DefaultClip;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourceSFX;

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

        if (PlayerPrefs.HasKey("SavedSoundVolume"))
        {
            float savedSoundVolume = PlayerPrefs.GetFloat("SavedSoundVolume");
            mainMixer.SetFloat("SoundsVolume", savedSoundVolume);
        }
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
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
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
