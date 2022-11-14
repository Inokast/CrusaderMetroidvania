using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private PlayerAnimation anim;
    [SerializeField] private Collider2D staffCol;

    [Header("AttackStats")]
    public int attackPower = 3;
    public float actionCooldown = .5f;

    public bool isAttacking = false;

    [Header("MagicStats")]
    public float magicPower = 3;
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
        
    }

    public void FillMagicCharge() 
    {
        magicCharge = maxMagicCharge;
    }

    private void Attack() 
    {
        print("Attack is called");
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
