using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }


    [Min(0f)]
    [SerializeField] float _speed;

    bool _isGameOver = false;

    public bool IsGameOver
    {
        get => _isGameOver; private set => _isGameOver = value;
    }

    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
    }
}