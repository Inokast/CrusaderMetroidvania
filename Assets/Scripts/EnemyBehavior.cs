using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public enum State { idle, chasing, attacking, dead };

public class EnemyBehavior : MonoBehaviour
{
    [Header("Objects")]
    GameObject player;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] Collider2D weapon;
    EnemyBehavior brain;
    [SerializeField] GameObject drop;

    [Header("Behavior Values")]
    State currentState;
    float distanceToPlayer;
    bool rightFacing = true;
    [SerializeField] float speed;
    [SerializeField] float chaseRange;

    [Header("Health Values")]
    [SerializeField] float enemyHealth;


    void Awake()
    {
        anim = GetComponent<Animator>();
        brain = GetComponent<EnemyBehavior>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = State.idle;
        rb = GetComponent<Rigidbody2D>();
        weapon.enabled = false;

        StartCoroutine(SetState());
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log($"Enemy health: {enemyHealth}; current state is: {currentState}"); //Deactivated for testing purposes -Dan
    }

    void FixedUpdate()
    {
        HandleStates();

        if (!rightFacing && player.transform.position.x < transform.position.x)
        {
            FlipSprite();
        }
        else if (rightFacing && player.transform.position.x > transform.position.x)
        {
            FlipSprite();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Projectile/Player"))     //may be contrasted with "Projectile/Enemy" in future installments...- T.E.
        {
            Destroy(col.gameObject);
            DamageEnemy(col.gameObject.GetComponent<Projectile>().power);
        }

        if (col.gameObject.CompareTag("Staff"))     //may be contrasted with "Projectile/Enemy" in future installments...- T.E.
        {
            DamageEnemy(9);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            currentState = State.attacking;
        }
    }

    void FlipSprite()
    {
        rightFacing = !rightFacing;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    #region state handling
    void HandleStates()
    {
        switch (currentState)
        {
            case State.idle:
                anim.SetBool("doWalking", false);
                weapon.enabled = false;
                break;
            case State.chasing:
                ChasePlayer();
                break;
            case State.attacking:
                AttackPlayer();
                break;
            default:
                Debug.LogError("error in switch statement for Enemy SetState");
                break;
        }
        StopCoroutine(SetState());
    }

    IEnumerator SetState()
    {
        while (true)
        {
            if (distanceToPlayer <= chaseRange)
            {
                currentState = State.chasing;
            }
            else
            {
                currentState = State.idle;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
    #endregion

    #region actions
    void ChasePlayer()
    {
        anim.SetBool("doWalking", true); anim.SetBool("doAttack", false);
        weapon.enabled = false;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        anim.SetBool("doWalking", false); anim.SetBool("doAttack", true);
        rb.velocity = Vector2.zero;
        weapon.enabled = true;
    }

    //for event trigger in animation
    void AttackSound(int s) 
    { 
        if (s == 0)
        {
           SFX.sfx.PlayGameSound(1);
        }
        if (s == 1)
        {
           SFX.sfx.PlayGameSound(2);
        }
    }

    public void DamageEnemy(float amt)
    {
        anim.SetTrigger("isHit");
        SFX.sfx.PlayGameSound(3);
        enemyHealth -= amt;

        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            Die();
        }
    }

    void DropLoot(int chance)
    {
        GameObject manaClone = drop;
        if (chance == 1)
        {
            //instantiate a mana potion here
            Instantiate(manaClone, transform.position, Quaternion.identity);
            Debug.Log($"You got a potion!");
        }
    }

    void Die()
    {
        int luck = Random.Range(1, 4);

        anim.SetTrigger("isDead");
        brain.enabled = false;
        
        if (gameObject.name == "Scyther")
        {
            DropLoot(1);
        }

        DropLoot(luck);
        Destroy(gameObject, 3f);
    }
    #endregion
}
