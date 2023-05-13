using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public delegate void OnGameStart();
public delegate void OnGameOver();

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField, Min(0f)] private float _speed = 5f;

    private bool _isGameOver;
    private bool _isGameStarted;
    int _score = 0;

    public AudioPlayer AudioPlayer { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    public static OnGameStart OnGameStart;
    public static OnGameOver OnGameOver;

    public bool IsGameOver
    {
        get => _isGameOver;
        private set => _isGameOver = value;
    }

    public bool IsGameStarted
    {
        get => _isGameStarted;
        private set => _isGameStarted = value;
    }

    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }

    public int Score{
        get => _score;
        private set => _score = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        AudioPlayer = GetComponent<AudioPlayer>();
    }

    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver.Invoke();
    }

    public void StartGame()
    {
        IsGameStarted = true;
        OnGameStart.Invoke();
    }

    public void IncreaseScore(){
        Score += 1;
        scoreText.text = $"{Score}";
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
