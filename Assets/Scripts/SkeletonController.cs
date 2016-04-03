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
    private GameObject playerLocal;
    private GameObject player2;

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
                transform.Translate(directionX * Time.deltaTime * movementSpeed, 0, directionZ * Time.deltaTime * movementSpeed);
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

    public void setHpSkeleton(int _newHp)
    {
        this.hpSkeleton = _newHp;
    }

    public int getHpSkeleton()
    {
        return this.hpSkeleton;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TraitFeu" || other.gameObject.tag == "BourrasqueInfernale" || other.gameObject.tag == "ChocAquatique" || other.gameObject.tag == "Obsidienne" || other.gameObject.tag == "FlecheMortelle" || other.gameObject.tag == "TornadeEnflammee")
        {
            IsAttacking = false;
            IsMoving = false;
            IsDead = true;
        }
    }
}
