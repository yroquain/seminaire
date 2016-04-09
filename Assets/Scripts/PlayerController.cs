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
    public int numeroJoueur;

    //sorts
    public bool IsImmolating;
    public GameObject Immo;
    public bool IsEole;

    //Moving in lava
    private bool IsOnLava;
    private bool IsOnGrass;
    private float heigh;

    //Animation triggers
    public bool IsAttacking;
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
    public float rotate;
    public Quaternion qTo = Quaternion.Euler(0.0f, 0.0f, 0.0f);

    //HUD
    float widthScreen;
    public GameObject CanvasJoueur;
    public GameObject Canvas;
    private Component[] Barre;
    private GameObject sort1mask;
    private GameObject sort2mask;
    public float CDsort1;
    public float finCDsort1;
    public float CDsort2;
    public float finCDsort2;
    public GameObject sort1;
    public GameObject sort2;
    public GameObject Element;
    private GameObject ElementAllie;
    public Sprite mageFeu;
    public Sprite mageEau;
    public Sprite mageAir;
    private GameObject MageClone;

    //Before game starts
    private bool IsGettingObject;
    public bool GameHasStarted;
    private GameObject TextReady;
    private GameObject ReadyText;
    private float refresh;
    private float WaitBeforeReady;

    //camera and trigger animation
    public GameObject MainCamera; //camera for players
    public GameObject SubCamera; //camera for cut-scenes
    #endregion

    #region Initialisation
    // Use this for initialization
    void Start()
    {
        WaitBeforeReady = Time.time;
        numeroJoueur = 1;
        if (GameObject.Find("Mage(Clone)") == null && this.gameObject.name == "LOCAL Player")
        {
            numeroJoueur = 0;
        }
        GameHasStarted = false;
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
        CanvasJoueur = Instantiate(Canvas);
        Barre = CanvasJoueur.GetComponentsInChildren<Image>();
        foreach (Image a in Barre)
        {
            if (a.name == "Mana2")
            {
                GetComponent<ManagementHpMana>().setManaBarre(a.gameObject);
            }
            if (a.name == "Mana1")
            {
                GetComponent<ManagementHpMana>().setManaBarreRef(a.gameObject);
            }
            if (a.name == "Health2")
            {
                GetComponent<ManagementHpMana>().setHealthBarre(a.gameObject);
            }
            if (a.name == "Health1")
            {
                GetComponent<ManagementHpMana>().setHealthBarreRef(a.gameObject);
            }
            if (a.name == "Sort1")
            {
                this.sort1 = a.gameObject;
            }
            if (a.name == "Sort2")
            {
                this.sort2 = a.gameObject;
            }
            if (a.name == "Element")
            {
                this.Element = a.gameObject;
            }
            if (a.name == "ElementAllie")
            {
                this.ElementAllie = a.gameObject;
            }

        }
        GetComponent<ManagementHpMana>().setHealthText(GameObject.Find("TextHealth").GetComponent<Text>());
        GetComponent<ManagementHpMana>().setManaText(GameObject.Find("TextMana").GetComponent<Text>());

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
        this.changerMage();


        widthScreen = Screen.width;

        sort1mask.SetActive(false);
        sort2mask.SetActive(false);

        CDsort1 = 0;
        CDsort2 = 0;
        IsGettingObject = true;

        if (numeroJoueur == 0)
        {
            string newTag = "";
            newTag = "Mage_Feu";
            this.sort1.GetComponent<Image>().sprite = sortfeu1;
            this.sort2.GetComponent<Image>().sprite = sortfeu2;

            this.sort1.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(5) + "";
            this.sort2.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(6) + "";

            this.Element.GetComponent<Image>().sprite = mageFeu;
            CmdChangerMage(newTag, this.gameObject);
        }
    }

   
    #endregion



    // Update is called once per frame
    void Update()
    {
        if (!GameHasStarted)
        {
            if (IsGettingObject)
            {
                TextReady = GameObject.Find("TextReady");
                ReadyText = GameObject.Find("ReadyText");
                if (TextReady != null)
                {
                    IsGettingObject = false;
                }
            }
            if (Input.GetButtonDown("Sort 1") && Time.time > WaitBeforeReady + 2f)
            {
                CmdIsReady(numeroJoueur);
                refresh = Time.time;
            }
            if (GameObject.Find("networkManager").GetComponent<GameController>().isReady[numeroJoueur])
            {
                TextReady.SetActive(true);
            }
            else
            {
                TextReady.SetActive(false);
            }
            if (Time.time > refresh + 0.5f)
            {
                CmdIsReadyRefresh(numeroJoueur, GameObject.Find("networkManager").GetComponent<GameController>().isReady[numeroJoueur]);
                refresh = Time.time;
            }
        }
        //Gestion hp et mort


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



        if (!IsUnderAnimation)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0); //Set X and Z velocity to 0
            if (Time.time > Attackelapsed + 0.9)
            {
                IsMoving = false;
                IsAttacking = false;
                CmdIsAttacking(numeroJoueur, false);
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
                }
            }
            else
            {
                IsMoving = false;
            }

            if (Input.GetAxis("Straffe") != 0 && !IsAttacking && !IsCasting)
            {

                transform.Translate(Input.GetAxis("Straffe") * Time.deltaTime * movementSpeed, 0, 0);
                IsMoving = true;
            }

            //When Attacking
            if (Input.GetButtonDown("Frapper") && !IsJumping)
            {
                Attackelapsed = Time.time;
                CmdIsAttacking(numeroJoueur, true);
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

            if (Input.GetButtonDown("Pause") && !IsCasting)
            {
                IsMoving = false;
                CanvasJoueur.GetComponent<scriptHUD>().showMenuPause();
                this.GetComponent<PlayerController>().enabled = false;
            }

            if (Input.GetAxis("KeepSpell") < 0.1)
            {
                IsCasting = false;
            }
            //For Yoann's test
            /*
            if(Input.GetKeyDown(KeyCode.J))
            {
                IsCasting = true;
            }*/

            //when casting spell 1
            if (Input.GetButtonDown("Sort 1") && !IsCasting && GameHasStarted)
            {
                if (Input.GetAxis("KeepSpell") > 0.9)
                {
                    IsCasting = true;
                }
                this.GetComponent<Sorts_simple>().CastSpell(1);
            }

            //when casting spell 2
            if (Input.GetButtonDown("Sort 2") && !IsCasting && GameHasStarted)
            {

                if (Input.GetAxis("KeepSpell") > 0.9)
                {
                    IsCasting = true;
                }
                this.GetComponent<Sorts_simple>().CastSpell(2);
            }
            if (Input.GetButtonDown("SwitchMage") && GameHasStarted)
            {
                if (CDsort1 == 0 && CDsort2 == 0 && !IsImmolating && !IsEole)
                {
                    this.changerMage();
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
            MainCamera.transform.position = new Vector3(-40f, 21f, -170.0f);
            MainCamera.transform.rotation = Quaternion.Euler(27.0f, 90.0f, 0.0f);
        }

    }


    //Fonction

    [Command]
    public void CmdDeadPlayer(GameObject myPlayer)
    {
        myPlayer.GetComponent<NetworkedPlayerScript>().RpcResolveDead();
    }
    [Command]
    public void CmdIsReady(int numeroJoueur)
    {
        this.GetComponent<NetworkedPlayerScript>().RpcIsReady(numeroJoueur);
    }
    [Command]
    public void CmdIsReadyRefresh(int numeroJoueur, bool ready)
    {
        this.GetComponent<NetworkedPlayerScript>().RpcIsReadyRefresh(numeroJoueur, ready);
    }

    void OnCollisionEnter(Collision col)
    {

        //the mage fall in the deeps
        if (col.gameObject.tag == "Fond_Ravin")
        {
            //the player die
            GetComponent<ManagementHpMana>().removeHp(GetComponent<ManagementHpMana>().getMaxHp());
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
                GetComponent<ManagementHpMana>().removeHp(GetComponent<ManagementHpMana>().getMaxHp());
            }

        }

        //Si le personnage marche sur les plateformes de l'enigme1 sans les avoir refroidies
        if (collision.gameObject.tag == "PlateformeEnigme1" && !this.IsImmolating)
        {
            if (collision.gameObject.GetComponent<Plateforme1>().IsDeadly)
            {
                GetComponent<ManagementHpMana>().removeHp(GetComponent<ManagementHpMana>().getMaxHp());
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
    public void StartGame()
    {
        CmdStartGame(this.gameObject);
        GameHasStarted = true;
        ReadyText.SetActive(false);
    }
    private void changerMage()
    {
        string newTag = "";
        if (this.gameObject.tag == "Mage_Feu" && this.gameObject.GetComponent<PlayerController>().IsImmolating)
        {
            this.gameObject.GetComponent<Sorts_simple>().CastSpell(2);
        }

        if (GameObject.FindGameObjectsWithTag("Mage_Feu").Length == 0)
        {
            newTag = "Mage_Feu";
            this.sort1.GetComponent<Image>().sprite = sortfeu1;
            this.sort2.GetComponent<Image>().sprite = sortfeu2;

            this.sort1.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(5) + "";
            this.sort2.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(6) + "";

            this.Element.GetComponent<Image>().sprite = mageFeu;
        }
        else if (GameObject.FindGameObjectsWithTag("Mage_Eau").Length == 0)
        {
            newTag = "Mage_Eau";
            this.sort1.GetComponent<Image>().sprite = sorteau1;
            this.sort2.GetComponent<Image>().sprite = sorteau2;

            this.sort1.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(3) + "";
            this.sort2.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(4) + "";

            this.Element.GetComponent<Image>().sprite = mageEau;
        }
        else if (GameObject.FindGameObjectsWithTag("Mage_Air").Length == 0)
        {
            newTag = "Mage_Air";
            this.sort1.GetComponent<Image>().sprite = sortair2;
            this.sort2.GetComponent<Image>().sprite = sortair1;

            this.sort1.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(1) + "";
            this.sort2.GetComponentInChildren<Text>().text = this.GetComponent<ManagementHpMana>().getCostManaSpell(2) + "";

            this.Element.GetComponent<Image>().sprite = mageAir;
        }
        CmdChangerMage(newTag, this.gameObject);
    }
    [Command]
    private void CmdStartGame(GameObject Player)
    {

        this.GetComponent<NetworkedPlayerScript>().RpcStartGame(Player);
    }
    [Command]
    public void CmdChangerMage(string newTag, GameObject thisPlayer)
    {

        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcChangerTenue(newTag, thisPlayer);
    }
    [Command]
    public void CmdIsAttacking(int numeroJoueur, bool _isActive)
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcSetIsAttacking(numeroJoueur, _isActive);
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

    #region Getter and Setter
    public bool getIsCasting()
    {
        return IsCasting;
    }
    public void setIsCasting(bool _isCasting)
    {
        this.IsCasting = _isCasting;
    }

    public bool getIsUnderAnimation()
    {
        return IsUnderAnimation;
    }
    public void setIsUnderAnimation(bool _isUnderAnimation)
    {
        this.IsUnderAnimation = _isUnderAnimation;
    }

    #endregion
}

