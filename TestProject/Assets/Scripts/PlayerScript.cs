
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float move_speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float jumpColdown = 0.5f;
    [SerializeField] float horizontalMove = 0f;
    [SerializeField] float verticalMove = 0f;
    [SerializeField] GameObject enemy;
    [SerializeField] HealthBar healthBar;
    [SerializeField] Button retryBtn;
    [SerializeField] string currentScene;

    public Animator animator;
    private bool isGrounded = true;
    private bool isDead = false;
    private bool isHitting = false;

    private float currentPlayerPos;
    private float jumpStart = 0;
    private float attackCoolDown = 0.7f;
    private float nextAttack = 0f;
    private int life = 100;

    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        healthBar.setMaxHealth(life);
        retryBtn.onClick.AddListener(ReloadScene);
        retryBtn.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            retryBtn.gameObject.SetActive(true);
            return;
        }
        isHitting = false;
        // Turn character?
        if (Input.GetAxisRaw("Horizontal") < 0)
            transform.localScale = new Vector2(-1f, 1f);
        else if (Input.GetAxisRaw("Horizontal") > 0)
            transform.localScale = new Vector2(1f, 1f);

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

        CheckLife();
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        //animator.SetBool("Grounded", false);
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
            if(Time.time >= nextAttack)
            {
                nextAttack = Time.time + attackCoolDown;
                animator.SetTrigger("Attack1");
                isHitting = true;
            }
            

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

    private void CheckLife()
    {
        if (enemy.GetComponent<Bandit>().isAttacking())
        {
            if (!animator.GetBool("IdleBlock")) // TODO: fix parry || !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1")
            {
                life -= 20;
                healthBar.setHealth(life);
                //animator.SetTrigger("Hurt");
                //Debug.Log("HIT");
            }
        }

     
        if(life <= 0f)
        {
            animator.SetTrigger("Death");
            isDead = true;
            //Debug.Log("DEAD");
        }
    }

    public bool isAttacking()
    {
        return isHitting;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneName: currentScene);
    }


}
