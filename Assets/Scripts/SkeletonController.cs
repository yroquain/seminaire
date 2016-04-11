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
    private Animator anim;
    private float Attackelapsed;

    public int hpSkeleton;
    private GameObject playerLocal;
    public int joueurAttack;


    //Rotation
    public float speed= 10f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
        hpSkeleton = 10;
        speed = 10f;
        anim = GetComponent<Animator>();
        IsAttacking = false;
        Attackelapsed = 0f;
        IsDead = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsCasting = false;
        IsActivate = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(hpSkeleton<=0)
        {
            isDead();
        }
        if (playerLocal == null)
        {
            if (GameObject.Find("LOCAL Player") != null)
            {
                playerLocal = GameObject.Find("LOCAL Player").gameObject;
            }
        }
        else if (IsActivate && !IsDead)
        {

            if (Time.time > Attackelapsed + 0.8f)
            {
                IsMoving = false;
                IsAttacking = false;
            }

            float difX = (playerLocal.transform.position.x - this.gameObject.transform.position.x) * (playerLocal.transform.position.x - this.gameObject.transform.position.x);
            float difZ = (playerLocal.transform.position.z - this.gameObject.transform.position.z) * (playerLocal.transform.position.z - this.gameObject.transform.position.z);
            if (difZ < 4 && difX < 4)
            {
                IsMoving = false;
            }

            int directionZ = 0;
            if(playerLocal.transform.position.z > this.gameObject.transform.position.z && difZ > 4)
            {
                directionZ = -1;
            }
            if (playerLocal.transform.position.z < this.gameObject.transform.position.z && difZ > 4)
            {
                directionZ = 1;
            }

            int directionX = 0;
            if (playerLocal.transform.position.x > this.gameObject.transform.position.x && difX > 4)
            {
                directionX = -1;
            }
            if (playerLocal.transform.position.x < this.gameObject.transform.position.x && difX > 4)
            {
                directionX = 1;
            }


            if (!IsAttacking && !IsCasting)
            {
                transform.Translate(directionX * Time.deltaTime * speed, 0, directionZ * Time.deltaTime * speed);
                IsMoving = true;
            }


            //Rotating
             Vector3 relativePos = playerLocal.transform.position - this.transform.position;
             Quaternion rotation = Quaternion.LookRotation(relativePos);
             transform.rotation = rotation;


            if (difZ < 4 && difX < 4)
            {
                Attackelapsed = Time.time;
                IsAttacking = true;
            }
            if (hpSkeleton <= 0)
            {
                IsDead = true;
                IsAttacking = false;
                IsMoving = false;
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

    public void isDead()
    {
        IsAttacking = false;
        IsMoving = false;
        IsDead = true;
    }
}
