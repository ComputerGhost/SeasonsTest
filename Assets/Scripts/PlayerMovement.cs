using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundCheck))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxJumpHeight = 2.5f;
    bool isJumpPressed;
    bool isJumping;

    [SerializeField] float walkSpeed = 3f;
    float targetVelocity;

    Rigidbody2D body;
    GroundCheck groundCheck;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.IsGrounded)
            isJumpPressed = true;
        if (Input.GetButtonUp("Jump"))
            isJumpPressed = false;

        targetVelocity = Input.GetAxis("Horizontal") * walkSpeed;
    }

    void FixedUpdate()
    {
        // Start the jump
        if (isJumpPressed && !isJumping) {
            var velocity = Mathf.Sqrt(2 * -Physics.gravity.y * body.gravityScale * maxJumpHeight);
            body.AddForce(Vector2.up * velocity * body.mass, ForceMode2D.Impulse);
            isJumping = true;
        }

        // Stop the jump early
        if (!isJumpPressed && isJumping) {
            if (body.velocity.y > 0)
                body.AddForce(Vector2.down * body.velocity.y * body.mass, ForceMode2D.Impulse);
            isJumping = false;
        }

        // Horizontal movement
        // I know it's weirded out math, but it works for now.
        var walkAcceleration = targetVelocity - body.velocity.x;
        body.AddForce(Vector2.right * walkAcceleration * body.mass, ForceMode2D.Impulse);
    }
}
