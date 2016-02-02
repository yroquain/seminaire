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

    //sorts
    private GameObject fireMage;
    private GameObject waterMage;
    private GameObject airMage;
    public bool IsImmolating;
    public GameObject Immo;


    //Moving in lava
    private bool IsOnLava;
    private bool IsOnGrass;
    private float heigh;

    //Animation triggers
    private bool IsAttacking;
    private bool IsMoving;
    private bool IsJumping;
    private float Attackelapsed;

    //Enigme2
    private bool IsUnderAnimation;
    private bool IsSavingCamInfo;
    private Vector3 CamPos;
    private Quaternion CamRot;
    public GameObject MainCamera;


    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    #endregion

    #region Initialisation
    // Use this for initialization
    void Start()
    {
        IsImmolating = false;
        IsUnderAnimation = false;
        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsRunning = true;
        IsOnGrass = true;
        IsOnLava = false;
        IsSavingCamInfo = true;
        rotate = 0;
        animationtoplay = "Run";
        Attackelapsed = 0.0f;
        anim = GetComponent<Animator>();


        if (this.gameObject.tag == "Mage_Feu")
        {
            fireMage = this.gameObject;
        }
        else if (this.gameObject.tag == "Mage_Eau")
        {
            waterMage = this.gameObject;
        }
        else if (this.gameObject.tag == "Mage_Air")
        {
            airMage = this.gameObject;
        }

    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        if (!IsUnderAnimation)
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
            if (Input.GetButtonDown("Frapper") && !IsJumping)
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


            //when casting spell 1
            if (Input.GetButtonDown("Sort 1"))
            {
                if (fireMage)
                    fireMage.GetComponent<Sorts_Feu>().CastSpell(1);

                if (waterMage)
                    waterMage.GetComponent<Sorts_Eau>().CastSpell(1);

                if (airMage)
                    airMage.GetComponent<Sorts_Air>().CastSpell(1);
            }

            //when casting spell 2
            if (Input.GetButtonDown("Sort 2"))
            {
                if (fireMage)
                    fireMage.GetComponent<Sorts_Feu>().CastSpell(2);

                if (waterMage)
                    waterMage.GetComponent<Sorts_Eau>().CastSpell(2);

                if (airMage)
                    airMage.GetComponent<Sorts_Air>().CastSpell(2);
            }

            //Immolation
            if (IsImmolating)
            {
                GetComponent<CapsuleCollider>().enabled = true;
                Immo.SetActive(true);
            }
            else
            {
                GetComponent<CapsuleCollider>().enabled = false;
                Immo.SetActive(false);
            }

            //When on lava
            if (IsOnLava)
            {
                transform.position = new Vector3(transform.position.x, heigh - 0.2f, transform.position.z);
            }
            miseAJourVarAnimation();
        }
        else
        {
            if (IsSavingCamInfo)
            {
                CamPos = MainCamera.transform.position;
                CamRot = MainCamera.transform.rotation;
                IsSavingCamInfo = false;
            }
            MainCamera.transform.position = new Vector3(28.95f, 21f, -170.0f);
            MainCamera.transform.rotation = Quaternion.Euler(27.0f, -90f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {

        //the mage fall in the deeps
        if (col.gameObject.tag == "Fond_Ravin")
        {
            //the player die
            HealthBar.HPBar.setCurHP(-10.0f);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Lave")
        {
            //if not fire type -> kill
            if (IsOnGrass && !this.IsImmolating)
            {
                heigh = transform.position.y;
                IsOnGrass = false;
                IsOnLava = true;
                HealthBar.HPBar.setCurHP(-10.0f);
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
    public void Animation()
    {
        if (IsUnderAnimation && !IsSavingCamInfo)
        {
            MainCamera.transform.position = CamPos;
            MainCamera.transform.rotation = CamRot;
            IsSavingCamInfo = true;
        }
        IsUnderAnimation = !IsUnderAnimation;
    }

}

