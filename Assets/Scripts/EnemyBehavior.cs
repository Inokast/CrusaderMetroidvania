using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public enum State { idle, chasing, attacking };

public class EnemyBehavior : MonoBehaviour
{
    [Header("Behavior Values")]
    GameObject player;
    State currentState;
    Rigidbody2D rb;
    [SerializeField] float speed;
    float distanceToPlayer;
    [SerializeField] float idleRange, chaseRange, attackRange;

    Health enemyHealth = new Health();
        

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = State.idle;
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(SetState());
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
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
        while (true)
        {
            if (distanceToPlayer == attackRange)
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
