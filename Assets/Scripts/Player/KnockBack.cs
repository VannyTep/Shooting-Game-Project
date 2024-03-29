using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private player_movement player_movement;

    [SerializeField] private float knockBackForce;
    private bool isKnockBackFromRight;

    private void Start() {
        player_movement = GetComponent<player_movement>();
    }

    void Update() {
        isKnockBackFromRight = player_movement.isFacingRight;
    }

    public void Knockback(Rigidbody2D rb2d) {
        if (isKnockBackFromRight == true) {
            rb2d.velocity = new Vector2(knockBackForce, rb2d.velocity.y);
        } else if (isKnockBackFromRight == false) {
            rb2d.velocity = new Vector2(-knockBackForce, rb2d.velocity.y);
        }
    }
}
