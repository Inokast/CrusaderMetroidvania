using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public class EnemyBehaveTest : MonoBehaviour
{
    [SerializeField] Transform ray;
    [SerializeField] LayerMask raymask;
    [SerializeField] float rayLength;
    [SerializeField] float cooldownTimer;
    [SerializeField] float attackingDistance;
    [SerializeField] float speed;

    RaycastHit2D hit;
    GameObject target;
    Animator anim;
    float distToPlayer;
    float cooldownTimerStart;
    bool attackMode, inAttackRange, coolingDown;


    void Awake()
    {
        anim = GetComponent<Animator>();
        cooldownTimerStart = cooldownTimer;
    }

    void Update()
    {
        if (inAttackRange)
        {
            hit = Physics2D.Raycast(ray.position, Vector2.left, rayLength, raymask);
            SeeRaycast();
        }

        //for player detection
        if (hit.collider != null)
        {
            Behave();
        }
        else if (hit.collider == null)
        {
            inAttackRange = false;
        }

        if (!inAttackRange)
        {
            anim.SetBool("doWalking", false);
            CeaseAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = col.gameObject;
            inAttackRange = true;
        }
    }

    void SeeRaycast()
    {
        if (distToPlayer > attackingDistance)
        {
            Debug.DrawRay(ray.position, Vector2.left * rayLength, Color.red);
        }
        else if (distToPlayer < attackingDistance)
        {
            Debug.DrawRay(ray.position, Vector2.left * rayLength, Color.green);
        }
    }

    //specific logic here
    void Behave()
    {
        distToPlayer = Vector2.Distance(transform.position, target.transform.position);

        if (distToPlayer > attackingDistance)
        {
            Approach();
            CeaseAttack();
        }
        else if (attackingDistance >= distToPlayer && !coolingDown)
        {
            AttackPlayer();
        }

        if (coolingDown)
        {
            CoolDown();
            anim.SetBool("doAttack", false);
        }
    }

    void CoolDown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0 && attackMode)
        {
            coolingDown = false;
            cooldownTimer = cooldownTimerStart;
        }
    }

    public void TriggerCooldown()
    {
        coolingDown = true;
    }

    void Approach()
    {
        anim.SetBool("doWalking", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
        {
            Vector2 targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    void AttackPlayer()
    {
        cooldownTimer = cooldownTimerStart;
        attackMode = true;
        anim.SetBool("doWalking", false); anim.SetBool("doAttack", true);

    }

    void CeaseAttack()
    {
        coolingDown = false;
        attackMode = false;
        anim.SetBool("doAttack", false);
    }
}
