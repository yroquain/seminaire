using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour {


    //Animation triggers
    private bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    private float movementSpeed = 10.0f;
    private Animator anim;

    private int hpSkeleton;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsCasting = false;
	}
	
	// Update is called once per frame
	void Update () {

        IsMoving = false ;
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!IsAttacking && !IsCasting)
            {
                transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
                IsMoving = true;
            }
        }
        miseAJourVarAnimation();
	}



    private void miseAJourVarAnimation()
    {
        anim.SetBool("isMoving", IsMoving);
        /*anim.SetBool("isWalking", IsWalking);
        anim.SetBool("isJumping", IsJumping);
        anim.SetBool("isAttacking", IsAttacking);
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
