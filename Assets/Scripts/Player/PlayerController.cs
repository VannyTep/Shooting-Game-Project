using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float movespeed;
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
        speedX = Input.GetAxisRaw("Horizontal") * movespeed;
        speedY = Input.GetAxisRaw("Vertical") * movespeed;

        rb.velocity = new Vector2(speedX, speedY).normalized * movespeed;
    }
}
