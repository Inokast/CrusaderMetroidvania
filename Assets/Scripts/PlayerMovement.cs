using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Assignment/Lab/Project: Metroidvania
//Name: Dan Sanchez
public class PlayerMovement : MonoBehaviour
{
    private PlayerCollision col;

    public Rigidbody2D rb;

    public PlayerAnimation anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce;
    public float fallMultiplier = 4.5f; // Fall multiplier helps give a good jump feel
    public float lowFallMultiplier = 2f;
    public float wallSlideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    private float x;
    private float y;

    [Space]
    [Header("Booleans")]
    public bool canMove = true;
    public bool wallJumped = false;
    public bool wallSlide = false;

    public bool fallMultiplierEnabled = true;
    
    //public bool isDashing; We might need this later

    private bool groundTouch;
    private bool hasDashed;
    private bool isDashing;

    public int side = 1; // Used to check which direction player is facing.


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        anim = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (rb.velocity.y < 0 && fallMultiplierEnabled == true) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && fallMultiplierEnabled == true) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) 
        {
            anim.SetTrigger("jump");

            if (col.onGround == true) 
            {
                //SFX.sfx.PlayGameSound(0);       //jump sfx- T.E.
                Jump(Vector2.up);
            }

            if (col.onWall && !col.onGround) 
            {
                WallJump();
            }
        }

        if (Input.GetButtonDown("Jump") && !hasDashed && col.onGround == false)
        {
            if (xRaw != 0 || yRaw != 0) 
            {
                Dash(xRaw, yRaw);
            }        
        }

        if (col.onGround)
        {
            wallJumped = false;
            wallSlide = false;
            hasDashed = false;
        }

        if (col.onWall && !col.onGround)
        {
            if (x != 0) 
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!col.onWall || col.onGround) 
        {
            wallSlide = false;
        }

        if (x > 0) 
        {
            side = 1;
            anim.Flip(side);
        }

        if (x < 0) 
        {
            side = -1;
            anim.Flip(side);
        }


    }

    private void Dash(float x, float y)
    {
        if (hasDashed == false) 
        {
            hasDashed = true;

            rb.velocity = Vector2.zero;
            Vector2 dir = new Vector2(x, y);

            rb.velocity += dir.normalized * dashSpeed;
            StartCoroutine(DashCooldown());
        }   
    }

    IEnumerator DashCooldown() 
    {
        rb.gravityScale = 0;
        fallMultiplierEnabled = false;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 3;
        fallMultiplierEnabled = true;
        isDashing = false;
    }

    private void FixedUpdate()
    {
        
        Vector2 dir = new Vector2(x, y);
        Move(dir);
       
    }

    public void Launch(Vector2 direction) 
    {
        print("Launch function called");

        rb.AddForce(direction * 10, ForceMode2D.Impulse);

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
