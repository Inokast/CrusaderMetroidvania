using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerAnimation anim;
    [SerializeField] private Collider2D staffCol;

    [Header("Spell Prefabs")]
    [SerializeField] private GameObject shootPrefab;

    [Header("AttackStats")]
    public int attackPower = 3;
    public float actionCooldown = .5f;

    public bool isAttacking = false;

    [Header("MagicStats")]
    public int magicPower = 3;
    public float maxMagicCharge = 100;
    public float magicCharge = 100;
    //public string[] equippedSpells;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<PlayerAnimation>();
        staffCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p")) 
        {
            print("Pressed P");
            Attack();
        }

        if (Input.GetKeyDown("1")) 
        {
            CastShoot();
        }
    }

    private void CastShoot() 
    {
        if (isAttacking == false && magicCharge >= 5) 
        {
            isAttacking = true;
            magicCharge -= 5;
            PlayerMovement player = GetComponent<PlayerMovement>();
            Vector2 temp;
            GameObject crystal;

            if (player.side == 1)
            {
                temp = new Vector2(transform.position.x + 2, transform.position.y + 2);
                crystal = Instantiate(shootPrefab, temp, shootPrefab.transform.rotation);
            }

            else
            {
                print("Side --");
                temp = new Vector2(transform.position.x - 2, transform.position.y + 2);
                crystal = Instantiate(shootPrefab, temp, shootPrefab.transform.rotation);
                crystal.transform.Rotate(-180, 0, 0);
            }


            Projectile p = crystal.GetComponent<Projectile>();
            p.power = magicPower;
            Rigidbody2D rb = crystal.GetComponent<Rigidbody2D>();
            print("crystal is made");

            Vector2 force = transform.right * 20;

            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(AttackCooldown());
        }      
    }

    public void FillMagicCharge() 
    {
        magicCharge = maxMagicCharge;
    }

    private void Attack() 
    {
        if (isAttacking == false) 
        {
            print("Attack is playing");
            isAttacking = true;
            anim.SetTrigger("attack");
            StartCoroutine(AttackCooldown());
        }     
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
