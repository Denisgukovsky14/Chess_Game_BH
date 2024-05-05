using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class MainMenuScript : MonoBehaviour
{
    private int currentSceneIndex;

    [SerializeField] private AudioClip DefaultMainMusic;
    [SerializeField] private AudioSource TapSound;

    public void PlayCurrentLevel()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OpenLevelList()
    {
        //GameObject.Find("MainTheme").GetComponent<MusicController>().SetMainTrack(Track);
        TapSound = GetComponent<AudioSource>(); 
        SceneController();
        Debug.Log(currentSceneIndex);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.LoadScene(2);
    }

    // Перенести в аудиоконтррлер

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }

    public void BackButtonOptions()
    {
        TapSound = GetComponent<AudioSource>();
        SceneManager.LoadScene( SceneControl.SN );
        Time.timeScale = 1;
    }

    public void ToOptions()
    {
        TapSound = GetComponent<AudioSource>();
        SceneController();
        Debug.Log(currentSceneIndex);
        SceneManager.LoadScene(3);
    }

    public void RestartLevel()
    {
        GameObject.Find("MainTheme").GetComponent<MusicController>().RestartTrack() ;
        TapSound = GetComponent<AudioSource>();
        SceneManager.LoadScene( SceneControl.CL );
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        GameObject.Find("MainTheme").GetComponent<MusicController>().SetMainTrack(DefaultMainMusic);
        TapSound = GetComponent<AudioSource>();
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        TapSound = GetComponent<AudioSource>();
        Application.Quit();
    }

    public void SceneController()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneControl.RememberNumber(currentSceneIndex);
    }


}
