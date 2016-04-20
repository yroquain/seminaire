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


	// Use this for initialization
	void Start () {

        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = true;
        IsCasting = false;
        IsRotating = false;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


        if (IsMoving)
        {
            delay -= Time.deltaTime;
            if (delay > 0)
            {
                // 6 = vitesse déplacement
                transform.Translate(x * Time.deltaTime * 6, 0, z * Time.deltaTime * 6);
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
                // 50.0f = speed
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 2, 0.0f), Time.deltaTime * 50.0f * 2);
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
