
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float move_speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpColdown = 1;
    private float jumpStart = 0;
    private bool isGrounded = true;
    private float currentPlayerPos;



    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveLeftAndRight();
       

        // Control when jump is pressed down
        if (Input.GetButtonDown("Jump") && isGrounded && (Time.time > jumpStart + jumpColdown) )
        {
            JumpPressed();
        }

        // Check if player as reached jump peak
        if (transform.position.y >= currentPlayerPos + 3)
        {
            player.gravityScale = 3f;
        }


        // Control when jump is released. Apply more gravity to get a faster descent.
        if (Input.GetButtonUp("Jump") )
        {
            AddGravity();
        }
    }
    
    //Checks if the player has landed on the ground or a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Platform")
        {
            isGrounded = true;
        }

        // Check if collision with roof
        if(collision.gameObject.name == "Roof")
        {
            AddGravity();
        }

        // Check Collision with spikes
        if(collision.gameObject.name == "Spikes")
        {
            //Do something to the player. 
        }
    }


    private void AddGravity()
    {
        player.gravityScale = 3f;
    }

    private void MoveLeftAndRight()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * move_speed;
        player.velocity = new Vector2(moveBy, player.velocity.y);
    }

    private void JumpPressed()
    {
        currentPlayerPos = transform.position.y;
        player.gravityScale = 1;
        player.velocity = new Vector2(0, jumpForce);
        isGrounded = false;
        jumpStart = Time.time;
    }



}
