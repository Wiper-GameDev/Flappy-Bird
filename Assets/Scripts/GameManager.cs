using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;




public class GameManager : MonoBehaviour
{

    [SerializeField] GameOver gameOverScreen;
    [SerializeField] private float _gameSpeed = 2.5f;

    public static GameManager Instance { get; private set; }

    private bool _isGameStarted;
    private bool _isGameOver;
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

        LoadBestScore();





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
        gameOverScreen.UpdateScoreBoard();
        SaveBestScore();
        IsGameOver = true;
        OnGameOver.Invoke();
    }


    public static bool IsScreenClickedOrTouched()
    {
        bool IsUIClicked = false;

        IsUIClicked = EventSystem.current.IsPointerOverGameObject();

        if (Input.touchCount > 0 && !IsUIClicked)
        {
            Touch touch = Input.touches[0];
            IsUIClicked = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }

        if (IsUIClicked) return false;

        return Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began);
    }


    void Update()
    {
        if (!IsScreenClickedOrTouched()) return;
        if (PauseMenu.IsGamePaused) return;
        if (IsGameStarted) return;
        StartGame();
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
        PlayerPrefs.SetInt("BestScore", Mathf.Max(Score, BestScore));
    }
}
