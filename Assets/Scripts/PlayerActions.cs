using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerAnimation anim;
    [SerializeField] private Collider2D staffCol;

    [Header("Spell Prefabs")]
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private GameObject shadowHandPrefab;

    [Header("PlayerStats")]
    public int health = 100;

    [Header("AttackStats")]
    public int attackPower = 3;
    public float actionCooldown = .5f;

    public bool isAttacking = false;

    [Header("MagicStats")]
    public int magicPower = 3;
    public float maxMagicCharge = 100;
    public float magicCharge = 100;

    [Header("Spell Stats")]
    [SerializeField] Transform blastPoint;
    [SerializeField] float blastRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LayerMask breakableLayer;

    [Header("Checkpoint Location")]
    public Vector2 checkpoint;

    private ManaBarManager manaBar;
    private HealthBarManager healthBar;
    //public string[] equippedSpells;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<PlayerAnimation>();
        staffCol.enabled = false;
        checkpoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        manaBar = FindObjectOfType<ManaBarManager>();
        healthBar = FindObjectOfType<HealthBarManager>();
        healthBar?.SetMaxHealth(health);
        healthBar?.UpdateHealth(health);
        manaBar?.SetMaxMana((int)maxMagicCharge);
        manaBar?.UpdateMana((int)magicCharge);

        GameManager.gm.CallPlayerSpawner(GameManager.gm.previousScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Attack();
        }

        if (Input.GetKeyDown("1")) 
        {
            CastShoot();
        }

        if (Input.GetKeyDown("2"))
        {
            CastShadowHand();
        }

        if (Input.GetKeyDown("3"))
        {
            CastBlast();
        }
    }

    #region Spellcasting
    private void CastShoot() 
    {
        SFX.sfx.PlayGameSound(4);

        if (isAttacking == false && magicCharge >= 5) 
        {
            isAttacking = true;
            magicCharge -= 5;
            PlayerMovement player = GetComponent<PlayerMovement>();
            Vector2 temp;
            GameObject crystal;

            if (player.side == 1)
            {
                temp = new Vector2(transform.position.x + 2, transform.position.y + 1);
                crystal = Instantiate(shootPrefab, temp, shootPrefab.transform.rotation);
            }

            else
            {
                temp = new Vector2(transform.position.x - 2, transform.position.y + 1);
                crystal = Instantiate(shootPrefab, temp, shootPrefab.transform.rotation);
                crystal.transform.Rotate(-180, 0, 0);
            }

            Projectile p = crystal.GetComponent<Projectile>();
            p.power = magicPower;
            Rigidbody2D rb = crystal.GetComponent<Rigidbody2D>();

            Vector2 force = transform.right * 20;

            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(AttackCooldown());
            manaBar.UpdateMana((int)magicCharge);
        }      
    }

    private void CastShadowHand()
    {
        SFX.sfx.PlayGameSound(4);

        if (GameManager.gm.unlockedShadowHand == true) 
        {
            if (isAttacking == false && magicCharge >= 5)
            {
                isAttacking = true;
                magicCharge -= 5;
                PlayerMovement player = GetComponent<PlayerMovement>();
                Vector2 temp;
                GameObject hand;

                if (player.side == 1)
                {
                    temp = new Vector2(transform.position.x + 1, transform.position.y - .1f);
                    hand = Instantiate(shadowHandPrefab, temp, shadowHandPrefab.transform.rotation);
                }

                else
                {
                    temp = new Vector2(transform.position.x - 1, transform.position.y - .1f);
                    hand = Instantiate(shadowHandPrefab, temp, shadowHandPrefab.transform.rotation);
                }

                Projectile p = hand.GetComponent<Projectile>();
                p.power = magicPower * 2;

                StartCoroutine(AttackCooldown());
                manaBar.UpdateMana((int)magicCharge);
            }
        }        
    }

    private void CastBlast()
    {
        SFX.sfx.PlayGameSound(4);

        isAttacking = true;
        magicCharge -= 10;
        anim.SetTrigger("attack");

        //Enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(blastPoint.position, blastRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBehavior>().DamageEnemy(15);
        }

        //BreakableWalls
        Collider2D[] hitBreakables = Physics2D.OverlapCircleAll(blastPoint.position, blastRange, breakableLayer);
        foreach (Collider2D breakable in hitBreakables)
        {
            Debug.Log("Hit a breakable");
            Destroy(breakable.gameObject);
        }

        StartCoroutine(AttackCooldown());
        manaBar.UpdateMana((int)magicCharge);
    }
    #endregion

    public void RefillMagicCharge(float amount) 
    {
        magicCharge += amount;
        magicCharge = Mathf.Clamp(magicCharge, 0, maxMagicCharge);
        manaBar.UpdateMana((int)magicCharge);
    }

    private void Attack() 
    {
        if (isAttacking == false) 
        {
            SFX.sfx.PlayGameSound(1);

            isAttacking = true;
            anim.SetTrigger("attack");
            StartCoroutine(AttackCooldown());
        }     
    }

    public void TakeDamage(int damage) 
    {
        health = health - damage;
        healthBar.UpdateHealth(health);
    }

    public void Death(int damage) 
    {
        TakeDamage(damage);
        if (health <= 0)
        {
            Respawn();
            health = 100;
            magicCharge = maxMagicCharge;
        }
    }

    public void Respawn() 
    {
        gameObject.transform.position = new Vector3(checkpoint.x, checkpoint.y, 0);
    }

    private void ToggleStaffHitbox() 
    {
        if (staffCol.enabled == false)
        {
            staffCol.enabled = true;
        }
    
        else if (staffCol.enabled == true) 
        {
            staffCol.enabled = false;
        }
    }

    IEnumerator AttackCooldown() 
    {
        yield return new WaitForSeconds(actionCooldown);
        isAttacking = false;
    }
}
