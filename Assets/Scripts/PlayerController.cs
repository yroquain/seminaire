using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{


    #region Variables
    //Movement
    private float movementSpeed = 10.0f;
    private float walkingSpeed = 6.0f;
    private float runningSpeed = 10.0f;
    private Animator anim;

    //sorts
    public bool IsImmolating;
    public GameObject Immo;
    public bool IsEole;


    //Moving in lava
    private bool IsOnLava;
    private bool IsOnGrass;
    private float heigh;

    //Animation triggers
    private bool IsAttacking;
    private bool IsMoving;
    private bool IsWalking;
    private bool IsJumping;
    private bool IsCasting;
    private float Attackelapsed;

    //Enigme2
    private bool IsUnderAnimation;
    private bool IsSavingCamInfo;
    private Vector3 CamPos;
    private Quaternion CamRot;
    public GameObject MainCamera;

    //Texture Mage
    public Material texture_air;
    public Material texture_eau;
    public Material texture_feu;

    //Sprite sorts
    public Sprite sortfeu1;
    public Sprite sortfeu2;
    public Sprite sorteau1;
    public Sprite sorteau2;
    public Sprite sortair1;
    public Sprite sortair2;

    //Rotation
    public float speed = 50.0f;
    private float rotate;
    private Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    //Canvas
    private GameObject CanvasJoueur;
    private Component[] Barre;
    private GameObject ManaBarre;
    public float qtmana;
    private float recupmana;
    private float manabarrex;
    private float manabarrey;
    private GameObject sort1mask;
    private GameObject sort2mask;
    public float CDsort1;
    public float finCDsort1;
    public float CDsort2;
    public float finCDsort2;
    private GameObject sort1;
    private GameObject sort2;
    #endregion

    #region Initialisation
    // Use this for initialization
    void Start()
    {
        IsImmolating = false;
        IsUnderAnimation = false;
        IsAttacking = false;
        IsMoving = false;
        IsJumping = false;
        IsWalking = false;
        IsOnGrass = true;
        IsOnLava = false;
        IsCasting = false;
        IsSavingCamInfo = true;
        IsEole = false;
        rotate = 0;
        Attackelapsed = 0.0f;
        anim = GetComponent<Animator>();
        if (gameObject.name == "LOCAL Player")
        {
            CanvasJoueur = GameObject.Find("CanvasJ1");
            GameObject.Find("CanvasJ2").gameObject.SetActive(false);
        }
        else
        {
            CanvasJoueur = GameObject.Find("CanvasJ2");
            GameObject.Find("CanvasJ1").gameObject.SetActive(false);
        }
        Barre = CanvasJoueur.GetComponentsInChildren<Image>();
        foreach(Image a in Barre)
        {
            if (a.name == "Mana2")
            {
                ManaBarre = a.gameObject;
            }
            if (a.name == "Sort1")
            {
                sort1 = a.gameObject;
            }
            if (a.name == "Sort2")
            {
                sort2 = a.gameObject;
            }

        }
        Barre = CanvasJoueur.GetComponentsInChildren<Button>();
        foreach (Button a in Barre)
        {
            if (a.name == "Sort2mask")
            {
                sort1mask = a.gameObject;
            }
            if (a.name == "Sort1mask")
            {
                sort2mask = a.gameObject;
            }
        }
        qtmana = 100;
        recupmana = Time.time;
        manabarrex = ManaBarre.GetComponent<RectTransform>().position.x;
        manabarrey = ManaBarre.GetComponent<RectTransform>().position.y;
        CmdChangerMage();
        sort1mask.SetActive(false);
        sort2mask.SetActive(false);
        CDsort1 = 0;
        CDsort2 = 0;
    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        if (CDsort1 > 0)
        {
            sort1mask.SetActive(true);
            sort1mask.GetComponentInChildren<Text>().text = CDsort1.ToString();
            if(Time.time>=finCDsort1+1)
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
        if (Time.time>= recupmana+1)
        {
            recupmana = Time.time;
            if(qtmana<100)
            {
                if(qtmana+5<100)
                {
                    qtmana += 5;
                }
                else
                {
                    qtmana = 100;
                }
            }
        }
        ManaBarre.GetComponent<RectTransform>().sizeDelta = new Vector2(2*qtmana, 10);
        ManaBarre.GetComponent<RectTransform>().position = new Vector3(manabarrex - (100- qtmana), manabarrey, 0);
        //int nombreJoueur=GameObject.FindGameObjectsWithTag("Mage_Eau").Length+GameObject.FindGameObjectsWithTag("Mage_Air").Length+GameObject.FindGameObjectsWithTag("Mage_Feu").Length;
        /*if (nombreJoueur ==1)
        {
            return;
        }*/
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
                if (!IsAttacking&&!IsCasting)
                {
                    transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
                    IsMoving = true;
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
            }

            //When Jumping
            if (GetComponent<Rigidbody>().velocity.y < 0.05 && GetComponent<Rigidbody>().velocity.y > -0.05 && Input.GetButtonDown("Jump") && !IsAttacking)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 250, 0), ForceMode.Force);
                IsJumping = true;
            }


            //Rotating
            if (Input.GetAxis("Horizontal") != 0 && !IsAttacking)
            {
                rotate += Input.GetAxis("Horizontal");
                qTo = Quaternion.Euler(0.0f, rotate, 0.0f);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, Time.deltaTime * speed);

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
            if (Input.GetAxis("KeepSpell") < 0.1)
            {
                IsCasting = false;
            }

            //when casting spell 1
            if (Input.GetButtonDown("Sort 1") && !IsCasting)
            {
                if (Input.GetAxis("KeepSpell")>0.9)
                {
                    IsCasting = true;
                }
                this.GetComponent<Sorts_simple>().CastSpell(1);
            }

            //when casting spell 2
            if (Input.GetButtonDown("Sort 2")&&!IsCasting)
            {

                if (Input.GetAxis("KeepSpell") > 0.9)
                {
                    IsCasting = true;
                }
                this.GetComponent<Sorts_simple>().CastSpell(2);
            }
            if (Input.GetButtonDown("SwitchMage"))
            {
                if (CDsort1 == 0 && CDsort2 == 0 && !IsImmolating && !IsEole)
                {
                    CmdChangerMage();
                }
            }

            //When on lava
            if (IsOnLava)
            {
                transform.position = new Vector3(transform.position.x, heigh - 0.2f, transform.position.z);
            }

            // On envoie les variables à l'animator pour qu'il joue la bonne animation
            miseAJourVarAnimation();
        }
        else
        {
            if (IsSavingCamInfo)
            {
                CamPos = MainCamera.transform.position;
                CamRot = MainCamera.transform.rotation;
                IsSavingCamInfo = false;
            }
            MainCamera.transform.position = new Vector3(28.95f, 21f, -170.0f);
            MainCamera.transform.rotation = Quaternion.Euler(27.0f, -90f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision col)
    {

        //the mage fall in the deeps
        if (col.gameObject.tag == "Fond_Ravin")
        {
            //the player die
            HealthBar.HPBar.setCurHP(-10.0f);
        }
    }

    void OnCollisionStay(Collision collision)
    {

        //Si le personnage rentre dans la lave
        if (collision.gameObject.tag == "Lave")
        {
            //if not fire type -> kill
            heigh = transform.position.y;
            IsOnGrass = false;
            IsOnLava = true;
            if (!this.IsImmolating)
            {
                HealthBar.HPBar.setCurHP(-10.0f);
            }

        }

        //Si le personnage marche sur les plateformes de l'enigme1 sans les avoir refroidies
        if (collision.gameObject.tag == "PlateformeEnigme1" && !this.IsImmolating)
        {
            if (collision.gameObject.GetComponent<Plateforme1>().IsDeadly)
            {
                HealthBar.HPBar.setCurHP(-10.0f);
            }
        }

        //Quand le personnage retourne sur la terre
        if (collision.gameObject.tag == "Grass")
        {
            IsOnGrass = true;
            IsOnLava = false;
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

    [Command]
    private void CmdChangerMage()
    {
        string newTag = "";
        if (this.gameObject.tag == "Mage_Feu" && this.gameObject.GetComponent<PlayerController>().IsImmolating)
        {
            this.gameObject.GetComponent<Sorts_simple>().CastSpell(2);
        }


        if (GameObject.FindGameObjectsWithTag("Mage_Feu").Length == 0)
        {
            newTag = "Mage_Feu";
            sort1.GetComponent<Image>().sprite = sortfeu1;
            sort2.GetComponent<Image>().sprite = sortfeu2;
        }
        else if (GameObject.FindGameObjectsWithTag("Mage_Eau").Length == 0)
        {
            newTag= "Mage_Eau";
            sort1.GetComponent<Image>().sprite = sorteau1;
            sort2.GetComponent<Image>().sprite = sorteau2;
        }
        else if (GameObject.FindGameObjectsWithTag("Mage_Air").Length == 0)
        {
            newTag = "Mage_Air";
            sort1.GetComponent<Image>().sprite = sortair1;
            sort2.GetComponent<Image>().sprite = sortair2;
        }
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcChangerTenue(newTag,this.gameObject);
    }

    public void Animation()
    {
        if (IsUnderAnimation && !IsSavingCamInfo)
        {
            MainCamera.transform.position = CamPos;
            MainCamera.transform.rotation = CamRot;
            IsSavingCamInfo = true;
        }
        IsUnderAnimation = !IsUnderAnimation;
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

