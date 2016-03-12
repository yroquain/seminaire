using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_simple : NetworkBehaviour
{

    public GameObject camera;
    public int numeroJoueur;
    public GameObject trait;
    public GameObject prerain;
    public Transform pos;
    public GameObject[] Prefabs;
    

    private float timeCast;
    private float timeCastMax = 2f;

    private int numberSpellCast;
    private bool castTraitDeFeu;
    private bool castPluieDivine;
    private bool castChocAquatique;
    private bool castMurEole;
    private bool castBourrasqueInfernale;
    private bool IsImmolating;
    private float timeimmo;
    private bool IsEole;
    private float timeeole;

    public GameObject wind;
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
        numeroJoueur = 0;
        if (GameObject.Find("Mage(Clone)") != null && this.gameObject.name=="LOCAL Player")
        {
            numeroJoueur = 1;
        }
        Isactivated = false;
        numberSpellCast = 0;
	}
	
	// Update is called once per frame
	void Update () {
       
        if (numberSpellCast != 0)
        {
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == true)
            {
                timeCast += Time.deltaTime;
            }
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == false || timeCast > timeCastMax)
            {
                if (numberSpellCast == 2 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    CmdBourrasqueInfernale();
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                }
                if (numberSpellCast == 3 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    CmdChocAquatique();
                    GetComponent<ManagementHpMana>().removeManaFromSpell(numberSpellCast);
                }
                if (numberSpellCast == 4 && GetComponent<ManagementHpMana>().getCurMana() >= GetComponent<ManagementHpMana>().getCostManaSpell(numberSpellCast))
                {
                    CmdPluieDivine();
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

        CmdIsCasting(this.gameObject.GetComponent<PlayerController>().getIsCasting(), numeroJoueur, numberSpellCast);
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
        eau1 = Time.time;
        gameObject.GetComponent<PlayerController>().CDsort1 = 3;
        gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
    }
    [Command]
    public void CmdPluieDivine()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcPluieDivine(this.gameObject);
        eau2 = Time.time;
        gameObject.GetComponent<PlayerController>().CDsort2 = 10;
        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
    }
    [Command]
    public void CmdBourrasqueInfernale()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcBourrasqueInfernale(this.gameObject);
        air2 = Time.time;
        gameObject.GetComponent<PlayerController>().CDsort2 = 5;
        gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
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