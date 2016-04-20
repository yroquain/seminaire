using UnityEngine;
using System.Collections;

public class MageCinematique : MonoBehaviour {

    private Animator anim;

    public bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;



	// Use this for initialization
	void Start () {

        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = true;
        IsCasting = false;

        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void moveMage(float x, float z, float delay){
        IsMoving = true;
        miseAJourVarAnimation();
        float timer = Time.time;
        while (Time.time - timer < delay)
        {
            transform.Translate(x * Time.deltaTime * 6, 0, z * Time.deltaTime * 6);
        }
        IsMoving = false;
        miseAJourVarAnimation();
    }

    public void rotateMage(float delay)
    {
        float timer = Time.time;
        while (Time.time - timer < delay)
        {
            // 50.0f = speed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 2, 0.0f), Time.deltaTime * 50.0f * 2);
        }
       
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
