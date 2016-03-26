using UnityEngine;
using System.Collections;

public class PCTuto : MonoBehaviour
{

    //Movement
    private float movementSpeed = 10.0f;
    private float walkingSpeed = 6.0f;
    private float runningSpeed = 10.0f;


    //Animation triggers
    public bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    private float Attackelapsed;
    private bool IsUnderAnimation;


    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!IsUnderAnimation)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0); //Set X and Z velocity to 0
            if (Time.time > Attackelapsed + 0.9)
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
                if (!IsAttacking && !IsCasting)
                {
                    transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
                    IsMoving = true;
                    if (!IsJumping)
                    {
                        GetComponent<Animation>().Play("Run");
                    }
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
                IsAttacking = true;
                GetComponent<Animation>().Play("StaffHit");
            }

            //When Jumping
            if (GetComponent<Rigidbody>().velocity.y < 0.05 && GetComponent<Rigidbody>().velocity.y > -0.05 && Input.GetButtonDown("Jump") && !IsAttacking)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 250, 0), ForceMode.Force);
                IsJumping = true;
                GetComponent<Animation>().Play("JumoRun");
            }
            if (!IsAttacking && !IsMoving && !IsJumping)
            {
                GetComponent<Animation>().Play("CombatModeA");
            }

            //Rotating
            if (Input.GetAxis("Horizontal") != 0 && !IsAttacking)
            {
                rotate += Input.GetAxis("Horizontal") * 2;
                qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed * 2);

            //Switching between running and walking
            if (Input.GetButtonDown("SwitchSpeed"))
            {
                if (IsWalking) //switch the speed
                {
                    movementSpeed = runningSpeed;
                }
                else
                {
                    movementSpeed = walkingSpeed;
                }
                IsWalking = !IsWalking;
            }


        }
    }
}
