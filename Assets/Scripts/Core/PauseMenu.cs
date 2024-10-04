// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PauseMenu : MonoBehaviour
// {
//     // Start is called before the first frame update
//     public GameObject pauseMenu;
//     public bool isPaused = false;
//     void Start()
//     {
//         pauseMenu.SetActive(false);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             if (isPaused)
//             {
//                 // Debug.Log("Escape key pressed 1");
//                 ResumeGame();
//             }
//             else
//             {
//                 // Debug.Log("Escape key pressed 2");
//                 PauseGame();
//             }
//         }
//     }

//     public void PauseGame()
//     {

//         pauseMenu.SetActive(true);
//         Time.timeScale = 0f;
//         isPaused = true;
//     }

//     public void ResumeGame()
//     {

//         pauseMenu.SetActive(false);
//         Time.timeScale = 1f;
//         isPaused = false;
//     }

//     public void QuitGame()
//     {
//         Application.Quit();
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);

        // Ensure the PauseManager is present in the scene
        if (GameObject.FindGameObjectWithTag("PauseManager") == null)
        {
            Debug.LogError("PauseManager not found in the scene. Ensure it's added.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Debug.Log("Escape key pressed");
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
