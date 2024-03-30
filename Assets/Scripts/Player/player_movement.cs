using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator animator;
    
    [Header("Movement System")]
    public float MoveSpeed;
    private float H_Move;

    [Header("Flip System")]
    public bool isFacingRight = true;

    [Header("Jump System")]
    public float jumpForce;
    [SerializeField] float jumpTime;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    bool isGrounded;

    Vector2 vecGravity;

    bool isJumping;
    float jumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vecGravity = new Vector2(0, Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update() {
        jump();

        IsGround();
    }

    void FixedUpdate() {
        move();

        if (H_Move > 0 && !isFacingRight) {
            flip();
        } else if (H_Move < 0 && isFacingRight) {
            flip();
        }
    }

    void move() {
        H_Move = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(H_Move));
        rb.velocity = new Vector2(H_Move * MoveSpeed * Time.deltaTime, rb.velocity.y);
    }

    void jump() {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            jumpCounter = 0;

            animator.SetTrigger("IsJump");
        }
        if (rb.velocity.y < 0 && isJumping) {
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpMultiplier = jumpMultiplier;

            if (t > 0.5f) {
                currentJumpMultiplier = jumpMultiplier * (1f - t);
            }

            rb.velocity += vecGravity * (currentJumpMultiplier - 1) * Time.deltaTime;
        }
        if(Input.GetButtonUp("Jump")) {
            isJumping = false;
            jumpCounter = 0;


            if (rb.velocity.y > 0) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }
        if (rb.velocity.y < 0) {
            rb.velocity -= vecGravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Animation

        if (isGrounded == true && rb.velocity.y == 0) {
            animator.SetBool("IsFall", false);
        }

        if (rb.velocity.y > 0.1f) {
            animator.SetBool("IsFall", false);
        }

        if (rb.velocity.y < -0.1f) {
            animator.SetBool("IsFall", true);
        }
    }

    void flip() {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void IsGround() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
