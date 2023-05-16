using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
public struct BirdAudioClips
{
    public AudioClip Wing;
    public AudioClip Hit;
    public AudioClip Point;
    public AudioClip Die;
    public AudioClip Swoosh;
}

public class Bird : MonoBehaviour
{

    // FIXME: Remove serialize field after getting correct values
    [SerializeField] private float maxRotationUpward;
    [SerializeField] private float maxRotationDownward;

    [SerializeField] private float flapForce;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallMulti;
    [SerializeField] private float gravityScale;
    [SerializeField] private GameObject sprite;



    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _canRotate = false;
    private AudioSource audioSource;

    // Input
    private bool _pressedFlap = false;

    // Animation Clips (We made a struct for easy to read)
    [SerializeField] BirdAudioClips audioClips;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = sprite.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // we don't want to fall onto ground or do any physics checking when we are not ready
        _rb.simulated = false;
        _rb.gravityScale = gravityScale;
        GameManager.OnGameStart.AddListener(OnGameStart);
        GameManager.OnGameOver.AddListener(OnGameOver);
        GameManager.OnGameRestarted.AddListener(OnGameRestart);
    }
    private void OnDestroy()
    {
        GameManager.OnGameStart.RemoveListener(OnGameStart);
        GameManager.OnGameOver.RemoveListener(OnGameOver);
        GameManager.OnGameRestarted.RemoveListener(OnGameRestart);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted) return;
        // Update the vertical velocity to clamp the fall speed
        _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -maxFallSpeed));


        // Dynamic gravity for realistic flap and fall
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = gravityScale * fallMulti;
        }
        else
        {
            _rb.gravityScale = gravityScale;
        }


        HandleAnimation();


        // Handle Rotation
        HandleRotation();

    }

    private void HandleAnimation()
    {
        // Pause bird animation either if game is over, or player is going down
        if (_rb.velocity.y <= -maxFallSpeed / 2 || GameManager.Instance.IsGameOver)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.speed == 0f) return;

            AnimationClip currentClip = _animator.GetCurrentAnimatorClipInfo(0)[0].clip;

            // Get the total number of frames in the animation clip
            int totalFrames = Mathf.RoundToInt(currentClip.length * currentClip.frameRate);

            // Calculate the current frame
            int currentFrame = Mathf.RoundToInt(stateInfo.normalizedTime * totalFrames) % totalFrames;

            if (currentFrame == 1)
            {
                _animator.speed = 0f;
            }
        }

        else
        {
            _animator.speed = 1f;
        }
    }

    private void ApplyFlap()
    {
        var force = flapForce;
        force -= _rb.velocity.y;
        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // Play Wing Audio
        audioSource.PlayOneShot(audioClips.Wing);
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

    public void OnFlapInput(InputAction.CallbackContext context)
    {
        // Don't register input either if game is not started, or game is over
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;
        if (!context.started) return;
        _pressedFlap = true;
    }

    private void OnGameStart()
    {
        transform.position *= Vector2.right;
        _canRotate = true;
        _rb.simulated = true;

        // Give a free flap to player
        _pressedFlap = true;
    }

    private void OnGameOver()
    {
        // Play Hit sound on either case
        audioSource.PlayOneShot(audioClips.Hit);
    }


    private void OnGameRestart(){
        // Reset position
        transform.position *= Vector2.right;

        // Disable rigid body
        _rb.simulated = false;

        // Reset rotation
        sprite.transform.rotation = Quaternion.identity;

        // Reset animation speed
        _animator.speed = 1f;
    }



    private void OnCollisionEnter2D(Collision2D other) // Always Ground and Pipes
    {
        if (!GameManager.Instance.IsGameOver)
            GameManager.Instance.GameOver();



        // Disable Rotation when we hit ground
        if (other.gameObject.CompareTag("Ground"))
        {
            _canRotate = false;
            GameManager.OnBirdHitGround.Invoke();
        }
        else
        {
            // We hit a pipe/barrier (Means got enough time to play die sound)
            audioSource.PlayOneShot(audioClips.Die);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Always A Score Point
    {
        if (GameManager.Instance.IsGameOver) return;
        GameManager.Instance.IncrementScore();
        audioSource.PlayOneShot(audioClips.Point);
    }
}
