using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Assignment/Lab/Project: Metroidvania
    //Name: Dan Sanchez

    private PlayerMovement playerMove;
    private PlayerActions playerAct;

    [Header("Layers")]
    public LayerMask groundLayer;


    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;


    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;


    // Start is called before the first frame update
    private void Start()
    {
        playerMove = gameObject.GetComponent<PlayerMovement>();
        playerAct = gameObject.GetComponent<PlayerActions>();
    }
    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy")) 
        {
            playerAct.Death(1);
            playerMove = gameObject.GetComponent<PlayerMovement>();
            Vector3 colVector = col.gameObject.transform.position;
            Vector3 temp = colVector - gameObject.transform.position;
            Vector2 dir = new Vector2(temp.x, temp.y).normalized;
            playerMove.Launch(dir);
            playerMove.anim.SetTrigger("hurt");
        }

        if (col.gameObject.CompareTag("Checkpoint")) 
        {
            playerAct.checkpoint = new Vector2(col.gameObject.transform.position.x, col.gameObject.transform.position.y);
        }

        if (col.gameObject.CompareTag("Hazard"))
        {
            print("Detected hazard box");
            playerAct.Death(10);
        }

        if (col.gameObject.CompareTag("PotionM"))
        {
            print("Previous Magic charge = " + playerAct.magicCharge);
            Destroy(col.gameObject);
            playerAct.RefillMagicCharge(15f);
            print("New Magic charge = " + playerAct.magicCharge);
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ShadowHand" & playerMove.jumpForce != 30) 
        {
            playerMove.jumpForce = 30;
        }
    }

    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "ShadowHand" & playerMove.jumpForce == 30)
        {
            playerMove.jumpForce = 15;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
