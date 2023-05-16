using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] HitEffect hitEffect;


    void Start()
    {
        GameManager.OnScoreUpdate.AddListener(OnScoreUpdate);
        GameManager.OnGameOver.AddListener(OnGameOver);
        GameManager.OnGameStart.AddListener(OnGameStart);

        // Disable game over screen
        gameOverScreen.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        GameManager.OnScoreUpdate.RemoveListener(OnScoreUpdate);
        GameManager.OnGameOver.RemoveListener(OnGameOver);
        GameManager.OnGameStart.RemoveListener(OnGameStart);
    }

    void OnScoreUpdate(int score)
    {
        scoreText.text = score.ToString();
    }

    void OnGameStart(){
        // scoreText.gameObject.SetActive(true);
    }

    void OnGameOver(){
        hitEffect.StartEffect();
        scoreText.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
    }

    public void RestartGame(){
        gameOverScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        GameManager.Instance.RestartGame();
    }
}
