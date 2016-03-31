using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour {


    //Animation triggers
    private bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    private bool IsActivate;
    private bool IsDead;
    private float movementSpeed = 10.0f;
    private Animator anim;
    private float Attackelapsed;

    private int hpSkeleton;

    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        hpSkeleton = 10;
        IsAttacking = false;
        Attackelapsed = 0f;
        IsDead = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsCasting = false;
        IsActivate = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (IsActivate && !IsDead)
        {
            if (Time.time > Attackelapsed + 0.8f)
            {
                IsMoving = false;
                IsAttacking = false;
            }
            /*if (Input.GetAxis("Vertical") != 0)
            {
                if (!IsAttacking && !IsCasting)
                {
                    transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
                    IsMoving = true;
                }
            }
            //Rotating
            if (Input.GetAxis("Horizontal") != 0 && !IsAttacking)
            {
                rotate += Input.GetAxis("Horizontal") * 2;
                qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed * 2);

      

            if (Input.GetButtonDown("Frapper") && !IsJumping)
            {
                Attackelapsed = Time.time;
                IsAttacking = true;
            }*/
            if (hpSkeleton <= 0)
            {
                IsDead = true;
            }
        }
        

        miseAJourVarAnimation();
	}



    private void miseAJourVarAnimation()
    {
        anim.SetBool("isMoving", IsMoving);
        anim.SetBool("isAttacking", IsAttacking);
        anim.SetBool("isDead", IsDead);
        /*anim.SetBool("isWalking", IsWalking);
        anim.SetBool("isJumping", IsJumping);
        anim.SetBool("isCasting", IsCasting);*/
    }

    public void setHpSkeleton(int _newHp)
    {
        this.hpSkeleton = _newHp;
    }

    public int getHpSkeleton()
    {
        return this.hpSkeleton;
    }
}
