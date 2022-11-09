using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public enum State { idle, chasing, attacking };

public class EnemyBehavior : MonoBehaviour
{
    [Header("Behavior Values")]
    GameObject player;
    State currentState;
    Rigidbody2D rb;
    [SerializeField] int health;
    [SerializeField] float speed;
    float distanceToPlayer;
    [SerializeField] float idleRange, chaseRange, attackRange;

    public int Health
    {
        get { return health; }
        set 
        { 
            health = value;
            if(health < 0)
            {
                health = 0;
            }
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = State.idle;
        rb = GetComponent<Rigidbody2D>();
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        StartCoroutine(SetState());
    }

    void Update()
    {
        Debug.Log(currentState);
    }

    void FixedUpdate()
    {
        HandleStates();
    }

    void HandleStates()
    {
        switch (currentState)
        {
            case State.idle:
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
        if (distanceToPlayer < chaseRange && distanceToPlayer >= attackRange)
        {
            currentState = State.attacking;
        }
        else if (distanceToPlayer < idleRange && distanceToPlayer >= chaseRange)
        {
            currentState = State.chasing;
        }
        else
        {
            currentState = State.idle;
        }

        yield return new WaitForSeconds(.1f);
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        Debug.Log("Chasing player");
    }

    void AttackPlayer()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Attacking player");
    }
}
