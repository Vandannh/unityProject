
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float move_speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpColdown = 0.5f;
    private float jumpStart = 0;
    private bool isGrounded = true;
    private float currentPlayerPos;
    [SerializeField] float horizontalMove = 0f;
    [SerializeField] float verticalMove = 0f;
    public Animator animator;


    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        // Turn character?
        if (Input.GetAxisRaw("Horizontal") < 0)
            transform.localScale = new Vector2(-0.7f, 0.7f);
        else if (Input.GetAxisRaw("Horizontal") > 0)
            transform.localScale = new Vector2(0.7f, 0.7f);

        MoveLeftAndRight();
        Attack();

        // Measures Y axis velocity
        verticalMove = player.velocity.y;
        animator.SetFloat("AirSpeedY", verticalMove);

        // Measures Horizontal speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * move_speed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Control when jump is pressed down
        if (Input.GetButtonDown("Jump") && isGrounded && (Time.time > jumpStart + jumpColdown) )
        {
            JumpPressed();
            animator.SetTrigger("Jump");
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
            animator.ResetTrigger("Jump");
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Grounded", false);
    }

    //Checks if the player has landed on the ground or a platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "Platform")
        {
            isGrounded = true;
            animator.SetBool("Grounded", true);
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
        if (animator.GetBool("IdleBlock") == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float moveBy = x * move_speed;
            player.velocity = new Vector2(moveBy, player.velocity.y);
        }
        
    }

    private void JumpPressed()
    {
        currentPlayerPos = transform.position.y;
        player.gravityScale = 1;
        player.velocity = new Vector2(0, jumpForce);
        isGrounded = false;
        jumpStart = Time.time;
        
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack1");
        }
        if(Input.GetKeyUp(KeyCode.X))
        {
            animator.ResetTrigger("Attack1");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            animator.ResetTrigger("Block");
            animator.SetBool("IdleBlock", false);
        }

    }



}
