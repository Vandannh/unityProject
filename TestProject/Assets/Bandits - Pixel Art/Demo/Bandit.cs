using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour
{

    [SerializeField] float speed = 4.0f;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float engageRange = 5f;
    
    private float coolDown = 2f;
    private float nextAttack = 0f;
    private float life = 60f;
    

    private Animator animator;

    // Use this later for checking if it is a hit on target
    private bool isHit = false;
    

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isHit = false;
        MoveToTarget();
        CheckIfCombat();
        CheckLife();
    }

    public void MoveToTarget()
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

    public void CheckIfCombat()
    {
        // If in range of taget, attack
        if (Vector3.Distance (transform.position, targetPlayer.position) <= attackRange)
        {
            // If previous attck animation is not finished then return
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return;
            }

            // Else: redo animation
            if(Time.time >= nextAttack)
            {
                nextAttack = Time.time + coolDown;
                animator.SetTrigger("Attack");
                isHit = true;
                
                coolDown = (float)Random.Range(1f, 4f) + (float)(Random.Range(0f,10f) /10f) ;
            }
           
            
        }
    }

    public void CheckLife()
    {
        if (targetPlayer.GetComponent<PlayerScript>().isAttacking())
        {
            if(Vector3.Distance(transform.position, targetPlayer.position) <= attackRange)
            {
                life -= 20f;
                Debug.Log("HIT ON bandit");
            }
        }
        if (life <= 0f)
        {
            Debug.Log("BANDIT DEAD");
        }

    }

    public bool isAttacking()
    {
        return isHit;
    }
}


