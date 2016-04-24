using UnityEngine;
using System.Collections;

public class SkeletonController : MonoBehaviour {


    //Animation triggers
    private bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    public bool IsActivate;
    private bool IsDead;
    private float timerDead;
    private Animator anim;
    private float Attackelapsed;

    public int hpSkeleton;
    private GameObject playerTarget;
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
        timerDead = 0f;
        IsDead = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsCasting = false;      
	}
	
	// Update is called once per frame
	void Update () {
        if(hpSkeleton<=0)
        {
            isDead();
        }
        if (IsDead)
        {
            timerDead += Time.deltaTime;
            if (timerDead > 5)
            {
                Destroy(this.gameObject);
                
            }
        }
        if (!IsActivate && GameObject.Find("triggerSkeletonAnim") == null && !GameObject.Find("LOCAL Player").GetComponent<PlayerController>().getIsUnderAnimation() && !GameObject.Find("Mage(Clone)").GetComponent<PlayerController>().getIsUnderAnimation())
        {
            IsActivate = true;
        }
        if (IsActivate && playerTarget == null)
        {
            if (GameObject.Find("LOCAL Player").GetComponent<ManagementHpMana>().numeroJoueur == joueurAttack)
            {
                playerTarget = GameObject.Find("LOCAL Player").gameObject;
            }

            if (GameObject.Find("Mage(Clone)").GetComponent<ManagementHpMana>().numeroJoueur == joueurAttack)
            {
                playerTarget = GameObject.Find("Mage(Clone)").gameObject;
            }
        }
        else if (IsActivate && !IsDead)
        {

            if (Time.time > Attackelapsed + 0.8f)
            {
                IsMoving = false;
                IsAttacking = false;
            }

            float difX = (playerTarget.transform.position.x - this.gameObject.transform.position.x) * (playerTarget.transform.position.x - this.gameObject.transform.position.x);
            float difZ = (playerTarget.transform.position.z - this.gameObject.transform.position.z) * (playerTarget.transform.position.z - this.gameObject.transform.position.z);
            if (difZ < 4 && difX < 4)
            {
                IsMoving = false;
            }

            //Rotating
            Vector3 relativePos = playerTarget.transform.position - this.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;

            if (!IsAttacking && !IsCasting && playerTarget.transform.position.z > -335)
            {
                transform.Translate(0, 0,  Time.deltaTime * speed);
                IsMoving = true;
            }


            if (difZ < 4 && difX < 4)
            {
                Attackelapsed = Time.time;
                IsAttacking = true;
            }
            if (hpSkeleton <= 0)
            {
                isDead();
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4, transform.position.z), Time.deltaTime / 3);
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
