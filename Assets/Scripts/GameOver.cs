using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public struct ScoreBoard{
    public TextMeshProUGUI score;
    public TextMeshProUGUI bestScore;
}


[RequireComponent(typeof(AudioSource))]
public class GameOver : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    [SerializeField] AudioSource audioSource;
    [SerializeField] ScoreBoard scoreBoard;

    void Start(){
        GameManager.OnGameOver.AddListener(UpdateScoreBoard);
    }

    void OnDestroy(){
        GameManager.OnGameOver.RemoveListener(UpdateScoreBoard);
    }

    void UpdateScoreBoard(){
        scoreBoard.score.text = GameManager.Instance.Score.ToString();
        scoreBoard.bestScore.text = GameManager.Instance.BestScore.ToString();
    }



    public void PlaySwooshSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
}
