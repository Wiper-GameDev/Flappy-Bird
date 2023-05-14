using UnityEngine;
using UnityEngine.Events;




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



#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        Application.targetFrameRate = 60;
#endif
    }

    public void StartGame()
    {
        IsGameStarted = true;
        OnGameStart.Invoke();
    }

    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver.Invoke();
    }



    private void Start()
    {

    }

    private void Update()
    {
        bool clickedOrTouched = Input.touchCount > 0 || Input.GetMouseButtonDown(0);

        if (!clickedOrTouched) return;


        if (!IsGameStarted)
        {
            StartGame();
        };

        if (IsGameOver)
        {
            RestartGame();
        }
    }


    public void RestartGame()
    {
        IsGameOver = false;
        IsGameStarted = false;
        Score = 0;


        OnGameRestarted.Invoke();
        StartGame();
    }


    public void IncrementScore()
    {
        Score += 1;
    }
}
