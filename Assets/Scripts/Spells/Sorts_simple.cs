using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_simple : NetworkBehaviour
{

    public GameObject Mycamera;
    public int numeroJoueur;
    public int numeroautreJoueur;
    public GameObject trait;
    public GameObject obsidienne;
    public GameObject prerain;
    public GameObject prefire;
    public GameObject pregib;
    public GameObject pretyphon;
    public Transform pos;
    public GameObject[] Prefabs;
    public GameObject sphere;
    private float initnumero;
    private bool IsIniti;
    public bool IsUsingSpell;
    private float timeUsingSpell;
    private int numberspellcastcombo;

    private float timeCast;
    private float timeCastMax = 2f;
    public bool ImmolatingSpell;
    private int numberSpellCast;
    private bool castTraitDeFeu;
    private bool castPluieDivine;
    private bool castChocAquatique;
    private bool castMurEole;
    private bool castBourrasqueInfernale;
    public bool IsImmolating;
    private float timeimmo;
    private bool IsEole;
    private float timeeole;
    private bool IsDone;
    private bool IsGoingComp;

    public GameObject wind;
    public GameObject windfire;
    public GameObject wave;
    public GameObject mur;
    private GameObject muractif;
    private bool Isactivated;

    //CD
    private float feu1;
    private float feu2;
    private float air1;
    private float air2;
    private float eau1;
    private float eau2;

    // Use this for initialization
    void Start () {
        numberspellcastcombo = 0;
        timeUsingSpell = 0;
        IsUsingSpell = false;
        IsIniti = false;
        initnumero = Time.time;
        IsGoingComp = true;
        IsDone = false;
        ImmolatingSpell = false;
        sphere.SetActive(false);
        numeroJoueur = 0;
        numeroautreJoueur = 1;
        if (GameObject.Find("Mage(Clone)") != null && this.gameObject.name=="LOCAL Player")
        {
            numeroJoueur = 1;
            numeroautreJoueur = 0;
        }
        Isactivated = false;
        numberSpellCast = 0;
	}
	
	// Update is called once per frame
	void Update () {
       if(this.gameObject.name=="Mage(Clone)" && Time.time> initnumero+2 && !IsIniti)
        {
            if(GameObject.Find("LOCAL Player").GetComponent<Sorts_simple>().numeroJoueur==0)
            {
                numeroJoueur = 1;
                numeroautreJoueur = 0;
            }
            else
            {
                numeroJoueur = 0;
                numeroautreJoueur = 1;
            }
            IsIniti = true;
        }
        if(Time.time> timeUsingSpell+0.5f && IsUsingSpell)
        {
            IsUsingSpell = false;
        }
        if (numberSpellCast != 0)
        {
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == true)
            {
                if (!IsDone)
                {
                    sphere.SetActive(true);
                    sphere.GetComponent<SphereCollide>().IsCollided = false;
                    IsDone = true;
                }
                timeCast += Time.deltaTime;
                GameObject autrejoueur = GameObject.Find("Mage(Clone)");
                if (autrejoueur != null)
                {
                    if (GameObject.Find("networkManager").GetComponent<GameController>().IsotherSpelling(numeroautreJoueur) && (autrejoueur.transform.position.x + autrejoueur.transform.position.z - transform.position.x - transform.position.z < 5) && (autrejoueur.transform.position.x + autrejoueur.transform.position.z - transform.position.x - transform.position.z > -5))
                    {
                        Component[] test = autrejoueur.gameObject.GetComponentsInChildren<Component>();
                        foreach (Component a in test)
                        {
                            if (a.gameObject.name == "Eternal Flame")
                            {
                                IsGoingComp = false;
                            }
                        }
                        if (IsGoingComp)
                        {
                            numberspellcastcombo = numberSpellCast;
                            SortCombine(numberSpellCast, GameObject.Find("networkManager").GetComponent<GameController>().numberSpell[numeroautreJoueur]);
                            numberSpellCast = 0;
                        }
                    }
                }
            }
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == false || timeCast > timeCastMax)
            {
                IsUsingSpell = true;
                IsDone = false;
                sphere.SetActive(false);
                timeUsingSpell = Time.time;
                if (numberSpellCast == 2 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    if (ImmolatingSpell)
                    {
                        air2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                        CmdTornadeEnflammee();
                    }
                    else
                    {
                        air2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                        CmdBourrasqueInfernale();
                    }
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                }
                if (numberSpellCast == 3 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                    CmdChocAquatique();
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                }
                if (numberSpellCast == 4 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    if (ImmolatingSpell)
                    {
                        eau2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                        CmdPluiedeFeu();
                    }
                    else
                    {
                        eau2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                        CmdPluieDivine();
                    }
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                }
                if (numberSpellCast == 5 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    feu1 = Time.time;
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                    CmdTraitDeFeu();

                }
                
                numberSpellCast = 0;
                timeCast = 0f;
                this.gameObject.GetComponent<PlayerController>().setIsCasting(false);
                CmdResetVarSpell(numeroJoueur);
                ImmolatingSpell = false;
                IsGoingComp = true;
            }
        }
        if (IsImmolating)
        {
            if (Time.time >= timeimmo + 1)
            {
                if (GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(6))
                {
                    GetComponent<ManagementHpMana>().removeManaFromSpell(6);
                    timeimmo = Time.time;
                }
                else
                {
                    CmdImmolation();
                    IsImmolating = false;
                    feu2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 2;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
            }
        }
        if (IsEole)
        {
            if (Time.time >= timeeole + 1)
            {
                if (GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(1))
                {
                    GetComponent<ManagementHpMana>().removeManaFromSpell(1);
                    timeeole = Time.time;
                }
                else
                {
                    CmdMurEole();
                    IsEole = false;
                    gameObject.GetComponent<PlayerController>().IsEole = false;
                    air1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
            }
        }


    }


    public void CastSpell(int numberSpell)
    {
        //Bourrasque infernale
        if (numberSpell == 1)
        {
            if (this.gameObject.tag == "Mage_Feu" && Time.time>=feu1+5){
                //Trait de feu
                numberSpellCast = 5;
            } else if (this.gameObject.tag == "Mage_Air"){
                //Mur d'Eole
                if(IsEole)
                {
                    CmdMurEole();
                    air1 = Time.time;
                    IsEole = false;
                    gameObject.GetComponent<PlayerController>().IsEole = false;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                if (!IsEole && Time.time >= air1 + 5)
                {
                    CmdMurEole();
                    IsEole = true;
                    gameObject.GetComponent<PlayerController>().IsEole = true;
                    timeeole = Time.time;
                }
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau1+3){
                //Choc aquatique
                numberSpellCast = 3;
            }
        } else if (numberSpell == 2)
        {   
            if (this.gameObject.tag == "Mage_Feu"){
                //Immolation
                if(IsImmolating)
                {
                    feu2 = Time.time;
                    CmdImmolation();
                    IsImmolating = false;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 2;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                if (!IsImmolating && Time.time >= feu2 + 2)
                {
                    CmdImmolation();
                    IsImmolating = true;
                    timeimmo = Time.time;
                }
            } else if (this.gameObject.tag == "Mage_Air" && Time.time>=air2+5){
                //Bourrasque Infernale
                numberSpellCast = 2;
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau2+10){
                //Pluie Divine
                numberSpellCast = 4;
            }
        }
        if (GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
        {
            CmdIsCasting(this.gameObject.GetComponent<PlayerController>().getIsCasting(), numeroJoueur, numberSpellCast);
            GameObject autrejoueur = GameObject.Find("Mage(Clone)");
            if (autrejoueur != null)
            {
                if (GameObject.Find("networkManager").GetComponent<GameController>().IsotherCasting(numeroautreJoueur) && (autrejoueur.transform.position.x + autrejoueur.transform.position.z - transform.position.x - transform.position.z < 4) && (autrejoueur.transform.position.x + autrejoueur.transform.position.z - transform.position.x - transform.position.z > -4))
                {
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                    if (numberSpellCast == 5)
                    {
                        feu1 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                        gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                    }
                    if (numberSpellCast == 3)
                    {
                        eau1 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                        gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                    }
                    if (numberSpellCast == 2)
                    {
                        air2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                    }
                    if (numberSpellCast == 4)
                    {
                        eau2 = Time.time;
                        gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                    }
                    numberSpellCast = 0;
                }
            }
        }
    }
    private void SortCombine(int sort1, int sort2)
    {
        IsUsingSpell = true;
        timeUsingSpell = Time.time;
        //SdB: Bourrasque infernale
        if (sort1 == 2)
        {
            //SC: Raz de marré
            if (sort2 == 3)
            {
                if (this.gameObject.tag == "Mage_Air")
                {
                    air2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                else
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                CmdRaz();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }
            //SC: Typhon
            if (sort2 == 4 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberspellcastcombo))
            {
                if (this.gameObject.tag == "Mage_Air")
                {
                    air2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                else
                {
                    eau2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                CmdTyphon();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }

        }
        //SdB: Choc Aquatique
        if (sort1 == 3)
        {
            //SC: Raz de marré
            if (sort2 == 2)
            {
                if (this.gameObject.tag == "Mage_Air")
                {
                    air2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                else
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                CmdRaz();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }
            //SC: Jet d'Obsidienne
            if (sort2 == 5 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberspellcastcombo))
            {
                if (this.gameObject.tag == "Mage_Feu")
                {
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                if (this.gameObject.tag == "Mage_Eau")
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                CmdJetObsidienne();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }
        }
        //SdB: Pluie Divine
        if (sort1 == 4)
        {
            //SC: Typhon
            if (sort2 == 2 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberspellcastcombo))
            {
                if (this.gameObject.tag == "Mage_Air")
                {
                    air2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                else
                {
                    eau2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                CmdTyphon();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);

            }
            //SC: Giboulée de feu
            if (sort2 == 5)
            {
                if (this.gameObject.tag == "Mage_Feu")
                {
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                else
                {
                    eau2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                CmdGiboulee();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }

        }
        //SdB: Trait de feu
        if (sort1 == 5)
        {
            //SC: Jet d'Obsidienne
            if (sort2 == 3 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberspellcastcombo))
            {
                if (this.gameObject.tag == "Mage_Feu")
                {
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                if (this.gameObject.tag == "Mage_Eau")
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                CmdJetObsidienne();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }
            //SC: Giboulée de feu
            if (sort2 == 4)
            {
                if (this.gameObject.tag == "Mage_Feu")
                {
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                else
                {
                    eau2 = Time.time;
                    gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                    gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
                }
                CmdGiboulee();
                GetComponent<ManagementHpMana>().removeManaFromSpell(numberspellcastcombo);
            }

        }
        numberspellcastcombo = 0;
        GetComponent<PlayerController>().setIsCasting(false);
        sphere.SetActive(false);
        CmdResetVarSpell(numeroJoueur);
        CmdResetVarSpell(numeroautreJoueur);
    }
    [Command]
    public void CmdIsCasting(bool _isCasting, int _numberPlayer, int _numberSpell)
    {
        this.GetComponent<NetworkedPlayerScript>().RpcSetIsCasting(_isCasting,_numberPlayer,_numberSpell);
    }
    [Command]
    public void CmdResetVarSpell(int _numberPlayer)
    {
        this.GetComponent<NetworkedPlayerScript>().RpcResetVarSpell(_numberPlayer);
    }
    [Command]
    private void CmdTraitDeFeu()
    {
        this.GetComponent<NetworkedPlayerScript>().RpcTraitDeFeu(this.gameObject);
    }
    [Command]
    private void CmdImmolation()
    {
        this.GetComponent<NetworkedPlayerScript>().RpcImmolation(this.gameObject);
    }
    [Command]
    public void CmdChocAquatique()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcChocAquatique(this.gameObject);
    }
    [Command]
    public void CmdJetObsidienne()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcJetObsidienne(this.gameObject);
    }
    [Command]
    public void CmdPluieDivine()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcPluieDivine(this.gameObject);
    }
    [Command]
    public void CmdPluiedeFeu()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcPluiedeFeu(this.gameObject);
    }
    [Command]
    public void CmdGiboulee()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcGiboulee(this.gameObject);
    }
    [Command]
    public void CmdTyphon()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcTyphon(this.gameObject);
    }
    [Command]
    public void CmdBourrasqueInfernale()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcBourrasqueInfernale(this.gameObject);
    }
    [Command]
    public void CmdTornadeEnflammee()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcTornadeEnflammee(this.gameObject);
    }
    [Command]
    public void CmdRaz()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcRaz(this.gameObject);
    }

    //Fonction Mur Eole
    [Command]
    public void CmdMurEole()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcMurEole(this.gameObject);
    }
    public bool getIsActivated()
    {
        return Isactivated;
    }
    public void setIsActivated(bool isActive)
    {
        this.Isactivated = isActive;
    }
    public void setMurActif(GameObject newWall)
    {
        this.muractif = newWall;
    }
    public GameObject getMurActif()
    {
        return this.muractif;
    }
}