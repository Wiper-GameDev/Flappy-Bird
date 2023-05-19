using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] GameObject resumeCountDown;
    [SerializeField] TextMeshProUGUI countdownText;
    Animator countdownAnimator;
    [SerializeField] float countdownAnimationLength = 1f;
    [SerializeField] AnimationClip countdownAnimation;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        resumeCountDown.SetActive(false);
        countdownAnimator = resumeCountDown.GetComponent<Animator>();
        countdownAnimator.speed = 0;
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
        StartCoroutine(ResumeAfterCountdown());
    }

    public void HomeButton()
    {
        IsGamePaused = false;
    }


    IEnumerator ResumeAfterCountdown()
    {
        resumeCountDown.SetActive(true);
        pauseMenuUI.SetActive(false);

        countdownAnimator.speed = countdownAnimation.length / countdownAnimationLength;

        for (int i = 3; i > 0; i--)
        {
            countdownAnimator.Play("Resume Countdown", 0, 0);
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(countdownAnimationLength);
        }


        resumeCountDown.SetActive(false);
        
        // Resume Game
        EnablePauseButton();
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

}
