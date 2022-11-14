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

    [Header("Behavior Values")]
    State currentState;
    [SerializeField] float speed;
    float distanceToPlayer;
    [SerializeField] float chaseRange, attackRange;
    bool rightFacing = true;    

    [Header("Health Values")]
    [SerializeField] float enemyHealth;


    void Awake()
    {
        anim = GetComponent<Animator>();
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
        Debug.Log($"Enemy health: {enemyHealth}");
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
        if (col.gameObject.CompareTag("Player"))
        {
            //DamageEnemy(10);
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
                Debug.Log("enemy is idle");
                break;
            case State.chasing:
                ChasePlayer();
                break;
            case State.attacking:
                AttackPlayer();
                break;
            default:
                Debug.Log("error in switch statement for Enemy SetState");
                break;
        }
        StopCoroutine(SetState());
    }

    IEnumerator SetState()
    {
        while (true)
        {
            if (distanceToPlayer <= chaseRange && distanceToPlayer >= attackRange)
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
        Debug.Log("Chasing player");
    }

    void AttackPlayer()
    {
        anim.SetBool("doWalking", false); anim.SetBool("doAttack", true);
        rb.velocity = Vector2.zero;
        weapon.enabled = true;
        Debug.Log("Attacking player");
    }

    void DamageEnemy(float amt)
    {
        enemyHealth -= amt;

        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //do death things here- T.E.
    }
    #endregion
}
