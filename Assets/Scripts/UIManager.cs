using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] HitEffect hitEffect;


    void Start()
    {
        GameManager.OnScoreUpdate.AddListener(OnScoreUpdate);
        GameManager.OnGameOver.AddListener(OnGameOver);
    }

    void OnDestroy()
    {
        GameManager.OnScoreUpdate.RemoveListener(OnScoreUpdate);
        GameManager.OnGameOver.RemoveListener(OnGameOver);
    }

    void OnScoreUpdate(int score)
    {
        scoreText.text = score.ToString();
    }

    void OnGameOver(){
        hitEffect.StartEffect();
    }
}
