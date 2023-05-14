using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    // FIXME: Remove serialize field after getting correct values
    [SerializeField] private float maxRotationUpward;
    [SerializeField] private float maxRotationDownward;

    [SerializeField] private float flapForce;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private GameObject sprite;


    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _canRotate = false;

    // Input
    private bool _pressedFlap = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = sprite.GetComponent<Animator>();
    }

    private void Start()
    {
        // we don't want to fall onto ground or do any physics checking when we are not ready
        _rb.simulated = false;
        GameManager.OnGameStart.AddListener(OnGameStart);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStart.RemoveListener(OnGameStart);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted) return;
        // Update the vertical velocity to clamp the fall speed
        _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -maxFallSpeed));

        // Handle Rotation
        HandleRotation();



        // Taking Input only when game is not over
        if (GameManager.Instance.IsGameOver)
        {
            return;
        };

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            _pressedFlap = true;
    }

    private void ApplyFlap()
    {
        var force = flapForce;
        force -= _rb.velocity.y;
        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }


    void HandleRotation()
    {
        if (!_canRotate) return;

        float target = 0;
        float rotationSpeed = 20f;

        if (GameManager.Instance.IsGameOver || _rb.velocity.y < 0f)
        {
            target = -_rb.velocity.y / maxFallSpeed * maxRotationDownward;
        }

        else if (_rb.velocity.y > 0f)
        {
            target = _rb.velocity.y / flapForce * maxRotationUpward;
            rotationSpeed = 40;
        }

        float smoothTime = Mathf.Clamp01(Time.smoothDeltaTime * rotationSpeed);

        sprite.transform.rotation = Quaternion.Slerp(sprite.transform.rotation, Quaternion.Euler(0, 0, target), smoothTime);
    }


    private void FixedUpdate()
    {
        if (_pressedFlap)
            ApplyFlap();

        _pressedFlap = false;
    }

    private void OnGameStart()
    {
        transform.position *= Vector2.right;
        _canRotate = true;
        _rb.simulated = true;
    }


    private void OnCollisionEnter2D(Collision2D other) // Always Ground and Pipes
    {
        GameManager.Instance.GameOver();

        // Disable Rotation when we hit ground
        if (other.gameObject.CompareTag("Ground"))
        {
            _canRotate = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Always A Score Point
    {
        GameManager.Instance.IncrementScore();
    }
}
