using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Dan Sanchez (Will add info later)
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private PlayerCollision col;

    public Rigidbody2D rb;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce;
    public float fallMultiplier = 4.5f;
    public float lowFallMultiplier = 2f;
    public float wallSlideSpeed = 5;
    public float wallJumpLerp = 10;

    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    public bool wallJumped;
    
    //public bool isDashing; We might need this later

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1; // Used to check which direction player is facing.


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        //anim = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) 
        {

            if (col.onGround == true) 
            {
                Jump(Vector2.up);
            }

            if (col.onWall && !col.onGround) 
            {
                WallJump();
            }
        }

        if (col.onGround)
        {
            wallJumped = false;
        }

        if (col.onWall && !col.onGround)
        {
            WallSlide();
        }


    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Move(dir);

        
    }

    private void Move(Vector2 dir)
    {
        if (!canMove) 
        {
            return;
        }


        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }

        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(dir.x * speed, rb.velocity.y), wallJumpLerp * Time.deltaTime);
        }

    }

    private void WallJump() 
    {
        if ((side == 1 && col.onRightWall) || side == -1 && !col.onRightWall)
        {
            side *= -1;
            
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = col.onRightWall ? Vector2.left : Vector2.right; // ? operand first checks if value on left is null before continuing to evaluate the bool

        Jump(Vector2.up / 1.5f + wallDir / 1.5f);

        wallJumped = true;
    }

    private void WallSlide() 
    {
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }

    private void Jump(Vector2 dir) 
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

}
