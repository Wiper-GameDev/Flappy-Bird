using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    Rigidbody2D RB;
    bool pressFlap = false;
    bool flying = false;

    [SerializeField] float maxRotationUpward;
    [SerializeField] float maxRotationDownward;

    [SerializeField] float flapForce;
    [SerializeField] float maxFallSpeed;
    Animator animator;

    const string IDLE_ANIM = "Idle";
    const string FLAP_ANIM = "Flap";

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -maxFallSpeed));

    }

    void HandleRotation(){
        // Rotation
        if (RB.velocity.y >= 0f){
            var target = maxRotationUpward - transform.rotation.z;
            var computed = RB.velocity.y / flapForce * target;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, computed), 0.5f);
        }
    }



    void FixedUpdate()
    {
        if (pressFlap)
        {
            // Play Flap Animation
            animator.Play(FLAP_ANIM);
            var force = flapForce;
            force -= RB.velocity.y;
            RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }


        if (pressFlap && !RB.simulated)
        {
            RB.simulated = true;
        }



        // Reset all inputs at the end
        pressFlap = false;

        HandleRotation();
    }

    public void OnFlap(InputAction.CallbackContext context)
    {
        if (!context.canceled) return;
        pressFlap = true;
    }
}
