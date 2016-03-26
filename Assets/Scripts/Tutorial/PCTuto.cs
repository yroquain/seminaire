using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    //Déclencheur des NPC
    public bool IsAskingForSpell;
    private float TimeAskingForSpell;

    //sorts
    public bool IsImmolating;
    public GameObject Immo;
    public bool IsEole;
    public bool isSpelling;
    public float timeSpelling;

    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);


    //Hud
    float widthScreen;
    public GameObject CanvasJoueur;
    private Component[] Barre;
    private GameObject sort1mask;
    private GameObject sort2mask;
    public float CDsort1;
    public float finCDsort1;
    public float CDsort2;
    public float finCDsort2;
    public Material mage;

    // Use this for initialization
    void Start()
    {
        transform.Find("Mage").GetComponent<Renderer>().material = mage;
        Barre = CanvasJoueur.GetComponentsInChildren<Button>();
        foreach (Button a in Barre)
        {
            if (a.name == "Sort1mask")
            {
                this.sort1mask = a.gameObject;
            }
            if (a.name == "Sort2mask")
            {
                this.sort2mask = a.gameObject;
            }
        }
        widthScreen = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>timeSpelling+1)
        {
            isSpelling = false;
        }
        if(Time.time> TimeAskingForSpell+0.2f)
        {
            IsAskingForSpell = false;
        }
        if (CDsort1 > 0)
        {
            sort1mask.SetActive(true);
            sort1mask.GetComponent<RectTransform>().position = new Vector3(.469f * widthScreen, 0.055f * widthScreen, 0);
            sort1mask.GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);
            sort1mask.GetComponentInChildren<Text>().text = CDsort1.ToString();
            if (Time.time >= finCDsort1 + 1)
            {
                finCDsort1 = Time.time;
                CDsort1 -= 1;
            }
        }
        else
        {
            sort1mask.SetActive(false);
        }
        if (CDsort2 > 0)
        {
            sort2mask.SetActive(true);
            sort2mask.GetComponent<RectTransform>().position = new Vector3(.531f * widthScreen, 0.055f * widthScreen, 0);
            sort2mask.GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);
            sort2mask.GetComponentInChildren<Text>().text = CDsort2.ToString();
            if (Time.time >= finCDsort2 + 1)
            {
                finCDsort2 = Time.time;
                CDsort2 -= 1;
            }
        }
        else
        {
            sort2mask.SetActive(false);
        }

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
            if (!IsAttacking && !IsCasting && !isSpelling)
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
        if (Input.GetButtonDown("Frapper") && !IsJumping && !isSpelling)
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
        if (!IsAttacking && !IsMoving && !IsJumping && !IsCasting && !isSpelling)
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

        /*if (Input.GetAxis("KeepSpell") < 0.1)
        {
            IsCasting = false;
        }*/
        if(Input.GetKeyDown(KeyCode.J))
        {
            IsCasting = true;
        }

        //when casting spell 1
        if (Input.GetButtonDown("Sort 1")/* && !IsCasting*/)
        {
            if (Input.GetAxis("KeepSpell") > 0.9)
            {
                IsCasting = true;
            }
            this.GetComponent<SortSimpleTuto>().CastSpell(1);
        }

        //when casting spell 2
        if (Input.GetButtonDown("Sort 2")/* && !IsCasting*/)
        {

            if (Input.GetAxis("KeepSpell") > 0.9)
            {
                IsCasting = true;
            }
            this.GetComponent<SortSimpleTuto>().CastSpell(2);
        }

        //When asking for a spell
        if (Input.GetButtonDown("SwitchMage") && !IsAskingForSpell)
        {
            IsAskingForSpell = true;
            TimeAskingForSpell = Time.time;
        }
        if(IsCasting)
        {
            GetComponent<Animation>().Play("Combat_Mode_C");
        }
    }
    public bool getIsCasting()
    {
        return IsCasting;
    }
    public void setIsCasting(bool _isCasting)
    {
        this.IsCasting = _isCasting;
    }
}

