using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Return if game is over
        if (GameManager.Instance.IsGameOver)
        {
            _rb.velocity = Vector2.zero;
            return;
        };
        

        // Return if GameObject is Pipe and game is not started
        if (gameObject.CompareTag("Pipes") && !GameManager.Instance.IsGameStarted) return;
        _rb.velocity = new Vector2(-GameManager.Instance.GameSpeed, 0f);
    }
}
