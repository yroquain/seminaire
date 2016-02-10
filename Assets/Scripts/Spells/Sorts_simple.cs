using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_simple : NetworkBehaviour
{

    public float range; //définit la portée de l'attaque
    public GameObject camera;

    public GameObject trait;
    public GameObject prerain;
    public Transform pos;
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private int currentPrefabIndex;
    private FireBaseScript currentPrefabScript;

    

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
        this.range = 100.0f;
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
                if (numberSpellCast == 1)
                {
                    CmdMurEole();
                }
                if (numberSpellCast == 2 && GetComponent<PlayerController>().qtmana >= 30)
                {
                    GetComponent<PlayerController>().qtmana -= 30;
                    CmdBourrasqueInfernale();
                }
                if (numberSpellCast == 3 && GetComponent<PlayerController>().qtmana >= 30)
                {
                    GetComponent<PlayerController>().qtmana -= 30;
                    CmdChocAquatique();
                }
                if (numberSpellCast == 4 && GetComponent<PlayerController>().qtmana >= 50)
                {
                    GetComponent<PlayerController>().qtmana -= 50;
                    CmdPluieDivine();
                }
                if (numberSpellCast == 5 && GetComponent<PlayerController>().qtmana>=50)
                {
                    GetComponent<PlayerController>().qtmana -= 50;
                    GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
                    Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
                        player.transform.position.y + 2,
                        player.transform.position.z + player.transform.forward.z * 2);
                    Instantiate(Prefabs[1], position, Quaternion.identity);
                    //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;*/
                    BeginEffect(0);
                }

                numberSpellCast = 0;
                timeCast = 0f;
                this.gameObject.GetComponent<PlayerController>().setIsCasting(false);
            }
        }
        if (IsImmolating)
        {
            if (Time.time >= timeimmo + 1)
            {
                if (GetComponent<PlayerController>().qtmana >= 15)
                {
                    GetComponent<PlayerController>().qtmana -= 15;
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
                if (GetComponent<PlayerController>().qtmana >= 15)
                {
                    GetComponent<PlayerController>().qtmana -= 15;
                    timeeole = Time.time;
                }
                else
                {
                    CmdMurEole();
                    IsEole = false;
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
                feu1 = Time.time;
                numberSpellCast = 5;
                gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
            } else if (this.gameObject.tag == "Mage_Air"){
                //Mur d'Eole
                if(IsEole)
                {
                    numberSpellCast = 1;
                    air1 = Time.time;
                    IsEole = false;
                    gameObject.GetComponent<PlayerController>().CDsort1 = 5;
                    gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
                }
                if (!IsEole && Time.time >= air1 + 5)
                {
                    numberSpellCast = 1;
                    IsEole = true;
                    timeeole = Time.time;
                }
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau1+3){
                //Choc aquatique
                eau1 = Time.time;
                numberSpellCast = 3;
                gameObject.GetComponent<PlayerController>().CDsort1 = 3;
                gameObject.GetComponent<PlayerController>().finCDsort1 = Time.time;
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
                air2 = Time.time;
                //Bourrasque Infernale
                numberSpellCast = 2;
                gameObject.GetComponent<PlayerController>().CDsort2 = 5;
                gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau2+10){
                //Pluie Divine
                eau2 = Time.time;
                numberSpellCast = 4;
                gameObject.GetComponent<PlayerController>().CDsort2 = 10;
                gameObject.GetComponent<PlayerController>().finCDsort2 = Time.time;
            }
        }
    }

    private void BeginEffect(int i)
    {
        currentPrefabIndex = i;
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = camera.transform.rotation;
                //rotation = transform.rotation;
                pos = new Vector3(transform.position.x + camera.transform.forward.x * 2,
                        transform.position.y + 2,
                        transform.position.z + camera.transform.forward.z * 2); ;
                //pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other friendly layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
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
    public void CmdPluieDivine()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcPluieDivine(this.gameObject);
    }
    [Command]
    public void CmdBourrasqueInfernale()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcBourrasqueInfernale(this.gameObject);
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