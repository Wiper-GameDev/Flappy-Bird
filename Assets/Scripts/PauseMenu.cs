using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private static bool _isGamePaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Button pauseButton;

    public static bool IsGamePaused
    {
        get { return _isGamePaused; }
        private set { _isGamePaused = value; }
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);
        EnablePauseButton();
        GameManager.OnGameOver.AddListener(DisablePauseButton);
        GameManager.OnGameRestarted.AddListener(EnablePauseButton);
    }

    void OnDestroy()
    {
        GameManager.OnGameOver.RemoveListener(DisablePauseButton);
        GameManager.OnGameRestarted.RemoveListener(EnablePauseButton);
    }

    bool CanPause()
    {
        return !GameManager.Instance.IsGameOver;
    }

    public void TogglePauseState()
    {
        if (IsGamePaused)
            ResumeGame();
        else
            PauseGame();
    }

    void DisablePauseButton()
    {
        pauseButton.interactable = false;
    }

    void EnablePauseButton()
    {
        pauseButton.interactable = true;
    }


    public void PauseGame()
    {
        if (!CanPause()) return;
        DisablePauseButton();
        Time.timeScale = 0f;
        IsGamePaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        EnablePauseButton();
        Time.timeScale = 1f;
        IsGamePaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void HomeButton()
    {
        IsGamePaused = false;
    }

}
