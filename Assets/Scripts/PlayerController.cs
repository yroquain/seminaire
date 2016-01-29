﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    #region Variables
    //Movement
    private float movementSpeed = 10.0f;
    private float walkingSpeed = 6.0f;
    private float runningSpeed = 10.0f;
    private bool IsRunning;
    private bool IsWalking;
    private string animationtoplay;
    private Animator anim;	

    //Moving in lava
    private bool IsOnLava;
    private bool IsOnGrass;
    private float heigh;

    //Animation triggers
    bool IsAttacking;
    bool IsMoving;
    bool IsJumping;
    private float Attackelapsed;

    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    #endregion

    #region Initialisation
    // Use this for initialization
    void Start()
    {
        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsRunning = true;
        IsOnGrass = true;
        IsOnLava = false;
        rotate = 0;
        animationtoplay = "Run";
        Attackelapsed = 0.0f;
        anim = GetComponent<Animator>();
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0); //Set X and Z velocity to 0
        if (Time.time > Attackelapsed + 1)
        {
            IsMoving = false;
            IsAttacking = false;
        }
        if (GetComponent<Rigidbody>().velocity.y < 0.05 && GetComponent<Rigidbody>().velocity.y > -0.05)
        {
            IsJumping = false;
        }

        //When Moving
        
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!IsAttacking)
            {
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
                /*if (!IsJumping)
                {
                    // GetComponent<Animation>().Play("Run");
                }*/
                IsMoving = true;
            }
        }
        else
        {
            IsMoving = false;
        }

        //When Attacking
        if ( Input.GetButtonDown("Frapper") && !IsJumping)
        {
            Attackelapsed = Time.time;
            //GetComponent<Animation>().Play("StaffHit");
            IsAttacking = true;
        }

        //When Jumping
        if (GetComponent<Rigidbody>().velocity.y < 0.05 && GetComponent<Rigidbody>().velocity.y > -0.05 && Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 250, 0), ForceMode.Force);
            IsJumping = true;
            //GetComponent<Animation>().Play("JumoRun");
        }

        //When not doing anything
        if (!IsAttacking && !IsMoving && !IsJumping)
            //GetComponent<Animation>().Play("CombatModeA");


        //Rotating
        if (Input.GetAxis("Horizontal") != 0 && !IsAttacking)
        {
            rotate += Input.GetAxis("Horizontal");
            qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed);

        //Switching between running and walking
    
        if (Input.GetButtonDown("SwitchSpeed"))
        {
            
            if (IsRunning)
            {
                movementSpeed = walkingSpeed;
                animationtoplay = "Walk";
            }
            if (IsWalking)
            {
                movementSpeed = runningSpeed;
                animationtoplay = "Run";
            }
            IsRunning = !IsRunning;
            IsWalking = !IsWalking;
        }

        //When on lava
        if (IsOnLava)
        {
            transform.position = new Vector3(transform.position.x, heigh - 0.2f, transform.position.z);
        }
        miseAJourVarAnimation();
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Lave")
        {
            //if not fire type -> kill
            if (IsOnGrass)
            {
                heigh = transform.position.y;
                IsOnGrass = false;
                IsOnLava = true;
            }

        }
        if (collision.gameObject.tag == "Grass")
        {
                IsOnGrass = true;
                IsOnLava = false;
        }
        
    }

    private void miseAJourVarAnimation()
    {
        anim.SetBool("isMoving", IsMoving);
        anim.SetBool("isWalking", IsWalking);
    }
}
