using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void RestartLevel(string level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
