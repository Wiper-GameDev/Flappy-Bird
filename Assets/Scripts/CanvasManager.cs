using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;


    void Start()
    {
        GameManager.OnScoreUpdate.AddListener(OnScoreUpdate);
    }

    void OnDestroy()
    {
        GameManager.OnScoreUpdate.RemoveListener(OnScoreUpdate);
    }

    void OnScoreUpdate(int score)
    {
        scoreText.text = score.ToString();
    }
}
