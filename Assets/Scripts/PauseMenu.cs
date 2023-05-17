using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private static bool _isGamePaused = false;
    [SerializeField] GameObject pauseMenuUI;

    public static bool IsGamePaused
    {
        get { return _isGamePaused; }
        private set { _isGamePaused = value; }
    }


    void PauseGame(){
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    void ResumeGame(){
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
}
