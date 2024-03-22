using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed;
    public Animator animator;

    float speedX, speedY;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(speedY));
        animator.SetFloat("speed", Mathf.Abs(speedX));
        
        speedX = Input.GetAxisRaw("Horizontal") * movespeed;
        speedY = Input.GetAxisRaw("Vertical") * movespeed;

    }

    void FixedUpdate()
    {
        move();
    }

    void move()
    { 
        rb.velocity = new Vector2(speedX, speedY).normalized * movespeed;
    }
}
