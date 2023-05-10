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
        // Initialization code goes here
    }

    void Update()
    {
        // Don't update anything if bird is dead
        if (_isDead) return;

        // Update the vertical velocity to clamp the fall speed
        RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -maxFallSpeed));

        // Play the appropriate animation based on the bird's orientation
        if ((315 >= transform.eulerAngles.z && transform.eulerAngles.z >= 275) || !RB.simulated)
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Handle collisions with the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            _isDead = true;
            _canRotate = false;
            _canFlap = false;

            GameData.Instance.GameOver();
        }
    }
}
