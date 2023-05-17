using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public struct ScoreBoard
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
    public Image newBestScoreIndicator;
}


[RequireComponent(typeof(AudioSource))]
public class GameOver : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    [SerializeField] AudioSource audioSource;
    [SerializeField] ScoreBoard scoreBoard;

    public void UpdateScoreBoard()
    {
        int currentScore = GameManager.Instance.Score;
        int oldBestScore = GameManager.Instance.BestScore;
        int newBestScore = Mathf.Max(oldBestScore, currentScore);

        if (newBestScore > oldBestScore)
            scoreBoard.newBestScoreIndicator.enabled = true;
        else
            scoreBoard.newBestScoreIndicator.enabled = false;   

        scoreBoard.score.text = currentScore.ToString();
        scoreBoard.bestScore.text = newBestScore.ToString();
    }


    public void PlaySwooshSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
}
