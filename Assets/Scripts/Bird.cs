using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    // Serialized fields
    [SerializeField] float maxRotationUpward;
    [SerializeField] float maxRotationDownward;
    [SerializeField] float flapForce;
    [SerializeField] float maxFallSpeed;

    GameObject handledPoint = null;

    // Components
    Rigidbody2D RB;
    Animator animator;

    // Flags
    bool _pressedFlap = false;
    bool _canRotate = false;
    bool _canFlap = true;
    bool _isDead = false;

    // Constants
    const string IDLE_ANIM = "Idle";
    const string FLAP_ANIM = "Flap";

    void Awake()
    {
        // Get components
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        // Update the vertical velocity to clamp the fall speed
        RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -maxFallSpeed));


        // Don't update anything if bird is dead
        if (_isDead) return;

        // Play the appropriate animation based on the bird's orientation
        if ((315 >= transform.eulerAngles.z && transform.eulerAngles.z >= 275))
            animator.Play(IDLE_ANIM);
        else
            animator.Play(FLAP_ANIM);
    }

    void FixedUpdate()
    {
        // Apply the flap force if the flap button was pressed and the bird is not dead
        if (_pressedFlap && !_isDead)
            ApplyFlap();

        // Start simulating the bird if the flap button was pressed and the bird is not yet simulated
        if (_pressedFlap && !RB.simulated)
            StartSimulating();

        // Handle the rotation of the bird
        HandleRotation();

        // Reset the flap flag to prevent continuous flapping
        _pressedFlap = false;
    }

    void ApplyFlap()
    {
        GameManager.Instance.AudioPlayer.PlayWing();
        // Calculate the force to be applied based on the flap force and current velocity
        var force = flapForce;
        force -= RB.velocity.y;
        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    void StartSimulating()
    {
        // Enable the simulation of the bird's rigidbody and enable rotation
        RB.simulated = true;
        _canRotate = true;

        // Enable pipe spawning and moving
        GameManager.Instance.StartGame();
    }

    void HandleRotation()
    {
        // Exit early if rotation is not allowed
        if (!_canRotate) return;

        // Calculate the target rotation based on the bird's velocity
        var target = maxRotationUpward - transform.rotation.z;
        var computed = RB.velocity.y / flapForce * target;

        // Smoothly rotate the bird towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, computed), 0.5f);
    }

    public void OnFlap(InputAction.CallbackContext context)
    {
        // Set the flap flag to true if the flap button was pressed
        if (!context.started) return;
        _pressedFlap = true;
    }

    void Die()
    {
        _isDead = true;
        _canFlap = false;
        animator.Play(IDLE_ANIM);
        GameManager.Instance.GameOver();

        GameManager.Instance.AudioPlayer.PlayHit();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Handle collisions with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            _canRotate = false;

            if (!_isDead)
                Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle collisions with the ground
        if ((other.gameObject.CompareTag("Pipe") || other.gameObject.CompareTag("Barrier")) && !_isDead)
        {
            RB.velocity = Vector2.zero;
            Die();
            GameManager.Instance.AudioPlayer.PlayDie();
        }

        if (!_isDead && other.gameObject.CompareTag("Point"))
        {
            if (other.gameObject == handledPoint) return;
            handledPoint = other.gameObject;
            GameManager.Instance.AudioPlayer.PlayPoint();
            GameManager.Instance.IncreaseScore();
        }
    }

}
