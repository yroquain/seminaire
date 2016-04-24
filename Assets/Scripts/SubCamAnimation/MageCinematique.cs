using UnityEngine;
using System.Collections;

public class MageCinematique : MonoBehaviour {

    private Animator anim;

    public bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    private bool IsRotating;

    private float delay;

    private float x;
    private float z;

    public float rotateSpeed { get; set; }
    public float speed { get; set; }


    // Use this for initialization
    void Start () {

        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = true;
        IsCasting = false;
        IsRotating = false;

        speed = 6.0f;
        rotateSpeed = 50.0f;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


        if (IsMoving)
        {
            delay -= Time.deltaTime;
            if (delay > 0)
            {
                transform.Translate(x * Time.deltaTime * speed, 0, z * Time.deltaTime * speed);
            }
            else
            {
                IsMoving = false;
                miseAJourVarAnimation();
            }
        }

        if (IsRotating)
        {
            delay -= Time.deltaTime;
            if (delay > 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 2, 0.0f), Time.deltaTime * rotateSpeed * 2);
            }
            else
            {
                IsRotating = false;
            }
        }
       
	}

    public void moveMage(float x, float z, float delay){
        this.delay = delay;
        this.x = x;
        this.z= z;
        IsMoving = true;
        miseAJourVarAnimation();
    }

    public void rotateMage(float delay)
    {
        this.delay = delay;
        IsRotating = true;
    }



    private void miseAJourVarAnimation()
    {
        anim.SetBool("isMoving", IsMoving);
        anim.SetBool("isWalking", IsWalking);
        anim.SetBool("isJumping", IsJumping);
        anim.SetBool("isAttacking", IsAttacking);
        anim.SetBool("isCasting", IsCasting);
    }
}
