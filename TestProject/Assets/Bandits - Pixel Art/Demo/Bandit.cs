using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float attackRange = 2f;

    private Animator animator;
    private Rigidbody2D enemy;
    private Sensor_Bandit groundSensor;
    private bool isGrounded = false;
    private bool idCombatIdle = false;
    private bool isDead = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {

        MoveToTarget();
        CheckIfCombat();




    }

    public void MoveToTarget()
    {

        if (Vector3.Distance (transform.position, targetPlayer.position) > attackRange)
        {
            if (targetPlayer.position.x >= transform.position.x + 1f)
            {
                 // Turn the enemy to target player
                transform.localScale = new Vector2(-1.0f, 1.0f);
                // Move player
                transform.Translate (new Vector3 (m_speed * Time.deltaTime, 0, 0));
                // Set animation
                animator.SetInteger("AnimState", 2);


            }
            else if (targetPlayer.position.x <= transform.position.x - 1f)
            {
                // Turn player
                transform.localScale = new Vector2(1.0f, 1.0f);
                // Move player
                transform.Translate (new Vector3 (-m_speed * Time.deltaTime, 0, 0));
                // set animation
                animator.SetInteger("AnimState", 2);

            }

        }
       

    }

    public void CheckIfCombat()
    {
        if (Vector3.Distance (transform.position, targetPlayer.position) <= attackRange)
        {
            // If previous attck animation is not finished then return
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return;
            }

            // Else: redo animation
            animator.SetTrigger("Attack");
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            
        }
    }
}


