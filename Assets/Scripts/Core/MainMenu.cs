using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SettingsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(9);
    }
}
