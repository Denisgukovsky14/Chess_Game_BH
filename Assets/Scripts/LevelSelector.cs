using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levels;

    public AudioClip Track;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levels.Length; i++)
            if (i + 1 > levelReached)
            {
                levels[i].interactable = false;
            }
    }

    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
        GameObject.Find("MainTheme").GetComponent<MusicController>().SetMainTrack(Track);
        SceneControl.CL = numberInBuild;
        Time.timeScale = 1;
    }


}