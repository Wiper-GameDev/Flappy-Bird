using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;




public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private bool _isGameStarted;
    private bool _isGameOver;
    [SerializeField] private float _gameSpeed = 2.5f;
    private int _score;



    # region Events
    public static UnityEvent OnGameStart;
    public static UnityEvent OnGameOver;
    public static UnityEvent OnGameRestarted;
    public static UnityEvent<int> OnScoreUpdate;
    public static UnityEvent OnBirdHitGround;
    #endregion

    #region Getters/setters

    public bool IsGameStarted
    {
        get => _isGameStarted;
        private set => _isGameStarted = value;
    }

    public bool IsGameOver
    {
        get => _isGameOver;
        private set => _isGameOver = value;
    }

    public float GameSpeed
    {
        get => _gameSpeed;
        private set => _gameSpeed = value;
    }

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreUpdate.Invoke(_score);
        }
    }

    public int BestScore { get; private set; }

    #endregion


    private void Awake()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (OnGameStart == null)
            OnGameStart = new UnityEvent();

        if (OnGameOver == null)
            OnGameOver = new UnityEvent();

        if (OnGameRestarted == null)
            OnGameRestarted = new UnityEvent();

        if (OnScoreUpdate == null)
        {
            OnScoreUpdate = new UnityEvent<int>();
        }

        if (OnBirdHitGround == null)
        {
            OnBirdHitGround = new UnityEvent();
        }





#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        Application.targetFrameRate = 60;
#endif
    }

    public void StartGame()
    {
        LoadBestScore();
        IsGameStarted = true;
        OnGameStart.Invoke();
    }

    public void GameOver()
    {
        BestScore = Mathf.Max(Score, BestScore);
        SaveBestScore();
        IsGameOver = true;
        OnGameOver.Invoke();
    }



    public void OnFlapInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;



        if (!IsGameStarted)
        {
            StartGame();
        }
    }


    public void RestartGame()
    {
        IsGameOver = false;
        IsGameStarted = false;
        Score = 0;


        OnGameRestarted.Invoke();
    }


    public void IncrementScore()
    {
        Score += 1;
    }


    void LoadBestScore()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
    }


    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
    }
}
