using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Assignment/Lab/Project: Metroidvania
    //Name: Dan Sanchez

    private Animator anim;
    private PlayerMovement move;
    private PlayerCollision col;

    private int currentSide = 1;
    [HideInInspector]


    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponentInParent<PlayerCollision>();
        move = GetComponentInParent<PlayerMovement>();  
    }
    // Update is called once per frame
    void Update()
    {
       anim.SetBool("onGround", col.onGround);
    }

    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);

    }



    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (currentSide != side) 
        {
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
            currentSide = side;
        }

       ////bool state = (side == 1) ? false : true;
       //if (side == 1 && transform.rotation.y != 0)
       //{
       //    transform.Rotate(transform.rotation.x, -180, transform.rotation.z);
       //}
       //
       //if (side == -1 && transform.rotation.y != 180)
       //{ transform.Rotate(transform.rotation.x, 180, transform.rotation.z); }

       //if (move.wallSlide)
       //{
       //    if (side == -1 && transform.rotation.y == 180)
       //        return;
       //
       //    if (side == 1 && transform.rotation.y == 0)
       //    {
       //        return;
       //    }
       //}

       


    }
}
