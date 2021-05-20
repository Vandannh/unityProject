﻿using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    [SerializeField] float speed = 4.0f;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float engageRange = 5f;
    [SerializeField] HealthBar healthBar;
    
    private Animator animator;
    private float coolDown = 1.5f;
    private float nextAttack = 0f;
    private int life = 60;
    private bool isInRange = false;
    private bool isDead = false;
    private bool isHittin = false;
    
    

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.setMaxHealth(life);
        healthBar.setHealth(life);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        isHittin = false;
        MoveToTarget();
        CheckIfCombat();
        CheckLife();
    }

    private void MoveToTarget()
    {

        if (Vector3.Distance (transform.position, targetPlayer.position) > attackRange && Vector3.Distance(transform.position, targetPlayer.position) < engageRange)
        {
            

            if (targetPlayer.position.x >= transform.position.x + 1f)
            {
                 // Turn the enemy to target player
                transform.localScale = new Vector2(-1.0f, 1.0f);
                // Move player
                transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
                // Set animation
                animator.SetInteger("AnimState", 2);


            }
            else if (targetPlayer.position.x <= transform.position.x - 1f)
            {
                // Turn player
                transform.localScale = new Vector2(1.0f, 1.0f);
                // Move player
                transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
                // set animation
                animator.SetInteger("AnimState", 2);

            }

        }
        else
        {
            //Not in range, stand idle
            animator.SetInteger("AnimState", 0);
        }
       

    }

    private void CheckIfCombat()
    {
        
        // If in range of taget, attack
        if (Vector3.Distance (transform.position, targetPlayer.position) <= attackRange)
        {
            isInRange = true;
            // If previous attck animation is not finished then return
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return;
            }

            // Else: redo animation
            if(Time.time >= nextAttack)
            {
                SoundManager.PlaySound("swing");
                nextAttack = Time.time + coolDown;
                animator.SetTrigger("Attack");
                isHittin = true;
                coolDown = (float)Random.Range(1f, 3f) + (float)(Random.Range(0f,10f) /10f) ;
            }
        }
        else
        {
            isInRange = false;
        }
        
    }

    private void CheckLife()
    {
        if (targetPlayer.GetComponent<PlayerScript>().isAttacking() && isInRange)
        {
            //BANDIT HIT
            //animator.SetTrigger("Hurt");
            if (targetPlayer.position.x < transform.position.x && targetPlayer.localScale.x > 0)
            {
                life -= 20;
                healthBar.setHealth(life);

            }
            else if (targetPlayer.position.x > transform.position.x && targetPlayer.localScale.x < 0)
            {
                life -= 20;
                healthBar.setHealth(life);
            }


        }
        if (life <= 0f)
        {
            //BANDIT DEAD
            SoundManager.PlaySound("dead");
            animator.SetTrigger("Death");
            isDead = true;
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

    }

    public bool isAttacking()
    {
        return isHittin;
    }
}


