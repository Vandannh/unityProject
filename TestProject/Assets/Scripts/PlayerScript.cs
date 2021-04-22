
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float move_speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpColdown = 1;
    float jumpStart = 0;
    private bool isGrounded = true;


    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Control left and right
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * move_speed;
        player.velocity = new Vector2(moveBy, player.velocity.y);

        // Control when jump is pressed down
        if (Input.GetButtonDown("Jump") && isGrounded && (Time.time > jumpStart + jumpColdown) )
        {
            player.gravityScale = 1;
            player.velocity = new Vector2(0, jumpForce);
            isGrounded = false;
            jumpStart = Time.time;
        }

        // Control when jump is released. Apply more gravity to get a faster descent.
        if (Input.GetButtonUp("Jump"))
        {
            player.gravityScale = 3f;
        }


    }
    
    //Checks if the player has landed on the ground or a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Platform")
        {
            isGrounded = true;
        }
    }

}
