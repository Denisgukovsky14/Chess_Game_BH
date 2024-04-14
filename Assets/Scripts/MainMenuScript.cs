using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ;

public class MainMenuScript : MonoBehaviour
{
    private int currentSceneIndex;


    public void PlayCurrentLevel()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OpenLevelList()
    {
        SceneController();
        Debug.Log(currentSceneIndex);
        SceneManager.LoadScene(2);
    }

    public void BackButtonOptions()
    {
        SceneManager.LoadScene( SceneControl.SN );
        Time.timeScale = 1;
    }

    public void ToOptions()
    {
        SceneController();
        Debug.Log(currentSceneIndex);
        SceneManager.LoadScene(3);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene( SceneControl.CL );
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SceneController()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneControl.RememberNumber(currentSceneIndex);
    }

}
