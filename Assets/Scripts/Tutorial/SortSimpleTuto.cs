using UnityEngine;
using System.Collections;

public class SortSimpleTuto : MonoBehaviour {

	 public GameObject Mycamera;
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
    public bool IsEole;
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

    public int otherspell;

    // Use this for initialization
    void Start () {
        timeUsingSpell = 0;
        IsUsingSpell = false;
        IsIniti = false;
        initnumero = Time.time;
        IsGoingComp = true;
        IsDone = false;
        ImmolatingSpell = false;
        sphere.SetActive(false);
        Isactivated = false;
        numberSpellCast = 0;
        otherspell = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time> timeUsingSpell+0.1f)
        {
            IsUsingSpell = false;
        }
        if (numberSpellCast != 0)
        {
            if (this.gameObject.GetComponent<PCTuto>().getIsCasting() == true)
            {
                if (!IsDone)
                {
                    sphere.SetActive(true);
                    sphere.GetComponent<SphereCollide>().IsCollided = false;
                    IsDone = true;
                }
                timeCast += Time.deltaTime;
               
            }
            if ((this.gameObject.GetComponent<PCTuto>().getIsCasting() == false || timeCast > timeCastMax) && otherspell==0)
            {
                GetComponent<PCTuto>().GetComponent<Animation>().Play("Spell_Cast_C");
                GetComponent<PCTuto>().isSpelling = true;
                GetComponent<PCTuto>().timeSpelling = Time.time;
                IsUsingSpell = true;
                IsDone = false;
                sphere.SetActive(false);
                timeUsingSpell = Time.time;
                if (numberSpellCast == 2)
                {
                    if (ImmolatingSpell)
                    {
                        //CmdTornadeEnflammee();
                    }
                    else
                    {
                        Vector3 position = new Vector3(transform.position.x + Mycamera.transform.forward.x * 2,
                            transform.position.y,
                            transform.position.z + Mycamera.transform.forward.z * 2);
                        Instantiate(wind, position, Quaternion.identity);
                        air2 = Time.time;
                        gameObject.GetComponent<PCTuto>().CDsort2 = 1;
                        gameObject.GetComponent<PCTuto>().finCDsort2 = Time.time;
                    }
                }
                if (numberSpellCast == 3)
                {
                    eau1 = Time.time;
                    gameObject.GetComponent<PCTuto>().CDsort1 = 1;
                    gameObject.GetComponent<PCTuto>().finCDsort1 = Time.time;
                    Vector3 position = new Vector3(pos.position.x,
                            pos.position.y,
                            pos.position.z);
                    Instantiate(trait, position, Quaternion.identity);
                }
                if (numberSpellCast == 4 )
                {
                    if (ImmolatingSpell)
                    {
                        //CmdPluiedeFeu();
                    }
                    else
                    {
                        eau2 = Time.time;
                        gameObject.GetComponent<PCTuto>().CDsort2 = 1;
                        gameObject.GetComponent<PCTuto>().finCDsort2 = Time.time;
                        Vector3 position = new Vector3((transform.position.x + Mycamera.transform.forward.x * 2),
                               transform.position.y + 2,
                               transform.position.z + Mycamera.transform.forward.z * 2);
                        Instantiate(prerain, position, Quaternion.identity);
                    }
                }
                if (numberSpellCast == 5 )
                {
                    feu1 = Time.time;
                    gameObject.GetComponent<PCTuto>().CDsort1 = 1;
                    gameObject.GetComponent<PCTuto>().finCDsort1 = Time.time;
                    GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
                    Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
                        player.transform.position.y + 2,
                        player.transform.position.z + player.transform.forward.z * 2);


                    Instantiate(Prefabs[1], position, Quaternion.identity);

                    Vector3 pos;
                    float yRot = transform.rotation.eulerAngles.y;
                    Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
                    Vector3 forward = transform.forward;
                    Vector3 right = transform.right;
                    Vector3 up = transform.up;
                    Quaternion rotation = Quaternion.identity;
                    GameObject currentPrefabObject = GameObject.Instantiate(GetComponent<SortSimpleTuto>().Prefabs[0]);
                    FireBaseScript currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

                    if (currentPrefabScript == null)
                    {
                        // temporary effect, like a fireball
                        currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
                        if (currentPrefabScript.IsProjectile)
                        {
                            // set the start point near the player
                            rotation = Mycamera.transform.rotation;
                            //rotation = transform.rotation;
                            pos = new Vector3(transform.position.x + Mycamera.transform.forward.x * 2,
                                    transform.position.y + 2,
                                    transform.position.z + Mycamera.transform.forward.z * 2); ;
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
                
                numberSpellCast = 0;
                timeCast = 0f;
                this.gameObject.GetComponent<PCTuto>().setIsCasting(false);
                ImmolatingSpell = false;
                IsGoingComp = true;
            }
            if(numberSpellCast!=0 && otherspell!=0)
            {
                GetComponent<PCTuto>().GetComponent<Animation>().Play("Spell_Cast_C");
                GetComponent<PCTuto>().isSpelling = true;
                GetComponent<PCTuto>().timeSpelling = Time.time;
                SortCombine(numberSpellCast, otherspell);
            }
        }


    }


    public void CastSpell(int numberSpell)
    {
        //Bourrasque infernale
        if (numberSpell == 1)
        {
            if (this.gameObject.tag == "Mage_Feu" && Time.time>=feu1+1){
                //Trait de feu
                numberSpellCast = 5;
            } else if (this.gameObject.tag == "Mage_Air"){
                //Mur d'Eole
                if(IsEole)
                {
                    if (!GetComponent<SortSimpleTuto>().getIsActivated())
                    {
                        GetComponent<SortSimpleTuto>().setMurActif(GameObject.Instantiate(GetComponent<SortSimpleTuto>().mur));
                        GetComponent<SortSimpleTuto>().getMurActif().transform.position = new Vector3(transform.position.x + GetComponent<SortSimpleTuto>().Mycamera.transform.forward.x * 2,
                            transform.position.y,
                            transform.position.z + GetComponent<SortSimpleTuto>().Mycamera.transform.forward.z * 2);
                        GetComponent<SortSimpleTuto>().getMurActif().transform.rotation = transform.rotation;

                    }
                    else
                    {
                        Destroy(GetComponent<SortSimpleTuto>().getMurActif().gameObject);
                    }
                    GetComponent<SortSimpleTuto>().setIsActivated(!GetComponent<SortSimpleTuto>().getIsActivated());
                    air1 = Time.time;
                    IsEole = false;
                    gameObject.GetComponent<PCTuto>().IsEole = false;
                    gameObject.GetComponent<PCTuto>().CDsort1 = 1;
                    gameObject.GetComponent<PCTuto>().finCDsort1 = Time.time;
                }
                if (!IsEole && Time.time >= air1 + 1)
                {
                    if (!GetComponent<SortSimpleTuto>().getIsActivated())
                    {
                        GetComponent<SortSimpleTuto>().setMurActif(GameObject.Instantiate(GetComponent<SortSimpleTuto>().mur));
                        GetComponent<SortSimpleTuto>().getMurActif().transform.position = new Vector3(transform.position.x + GetComponent<SortSimpleTuto>().Mycamera.transform.forward.x * 2,
                            transform.position.y,
                            transform.position.z + GetComponent<SortSimpleTuto>().Mycamera.transform.forward.z * 2);
                        GetComponent<SortSimpleTuto>().getMurActif().transform.rotation = transform.rotation;

                    }
                    else
                    {
                        Destroy(GetComponent<SortSimpleTuto>().getMurActif().gameObject);
                    }
                    GetComponent<SortSimpleTuto>().setIsActivated(!GetComponent<SortSimpleTuto>().getIsActivated());
                    IsEole = true;
                    gameObject.GetComponent<PCTuto>().IsEole = true;
                    timeeole = Time.time;
                }
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau1+1){
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
                    GetComponent<PCTuto>().IsImmolating = !GetComponent<PCTuto>().IsImmolating;

                    if (GetComponent<PCTuto>().IsImmolating)
                    {
                        GetComponent<CapsuleCollider>().enabled = true;
                        GetComponent<PCTuto>().Immo.SetActive(true);
                    }
                    else
                    {
                        GetComponent<CapsuleCollider>().enabled = false;
                        GetComponent<PCTuto>().Immo.SetActive(false);
                    }
                    IsImmolating = false;
                    gameObject.GetComponent<PCTuto>().CDsort2 = 1;
                    gameObject.GetComponent<PCTuto>().finCDsort2 = Time.time;
                }
                if (!IsImmolating && Time.time >= feu2 + 1)
                {
                    GetComponent<PCTuto>().IsImmolating = !GetComponent<PCTuto>().IsImmolating;

                    if (GetComponent<PCTuto>().IsImmolating)
                    {
                        GetComponent<CapsuleCollider>().enabled = true;
                        GetComponent<PCTuto>().Immo.SetActive(true);
                    }
                    else
                    {
                        GetComponent<CapsuleCollider>().enabled = false;
                        GetComponent<PCTuto>().Immo.SetActive(false);
                    }
                    IsImmolating = true;
                    timeimmo = Time.time;
                }
            } else if (this.gameObject.tag == "Mage_Air" && Time.time>=air2+1){
                //Bourrasque Infernale
                numberSpellCast = 2;
            } else if (this.gameObject.tag == "Mage_Eau" && Time.time>=eau2+1){
                //Pluie Divine
                numberSpellCast = 4;
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
                Vector3 position = new Vector3(transform.position.x,
              transform.position.y,
              transform.position.z);
                Instantiate(wave, position, Quaternion.identity);
            }
            //SC: Typhon
            if (sort2 == 4)
            {
                Vector3 position = new Vector3((transform.position.x + Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + Mycamera.transform.forward.z * 2);
                Instantiate(pretyphon, position, Quaternion.identity);
            }
            gameObject.GetComponent<PCTuto>().CDsort2 = 1;
            gameObject.GetComponent<PCTuto>().finCDsort2 = Time.time;

        }
        //SdB: Choc Aquatique
        if (sort1 == 3)
        {
            //SC: Raz de marré
            if (sort2 == 2)
            {
                Vector3 position = new Vector3(transform.position.x,
              transform.position.y,
              transform.position.z);
                Instantiate(wave, position, Quaternion.identity);
            }
            //SC: Jet d'Obsidienne
            if (sort2 == 5)
            {
                Vector3 position = new Vector3(pos.position.x,
                pos.position.y,
                pos.position.z);
                Instantiate(obsidienne, position, Quaternion.identity);
            }
            gameObject.GetComponent<PCTuto>().CDsort1 = 1;
            gameObject.GetComponent<PCTuto>().finCDsort1 = Time.time;
        }
        //SdB: Pluie Divine
        if (sort1 == 4)
        {
            //SC: Typhon
            if (sort2 == 2)
            {
                Vector3 position = new Vector3((transform.position.x + Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + Mycamera.transform.forward.z * 2);
                Instantiate(pretyphon, position, Quaternion.identity);

            }
            //SC: Giboulée de feu
            if (sort2 == 5)
            {
                Vector3 position = new Vector3((transform.position.x + Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + Mycamera.transform.forward.z * 2);
                Instantiate(pregib, position, Quaternion.identity);
            }
            gameObject.GetComponent<PCTuto>().CDsort2 = 1;
            gameObject.GetComponent<PCTuto>().finCDsort2 = Time.time;

        }
        //SdB: Trait de feu
        if (sort1 == 5)
        {
            //SC: Jet d'Obsidienne
            if (sort2 == 3)
            {
                Vector3 position = new Vector3(pos.position.x,
                pos.position.y,
                pos.position.z);
                Instantiate(obsidienne, position, Quaternion.identity);
            }
            //SC: Giboulée de feu
            if (sort2 == 4)
            {
                Vector3 position = new Vector3((transform.position.x + Mycamera.transform.forward.x * 2),
               transform.position.y + 2,
               transform.position.z + Mycamera.transform.forward.z * 2);
                Instantiate(pregib, position, Quaternion.identity);
            }
            gameObject.GetComponent<PCTuto>().CDsort1 = 1;
            gameObject.GetComponent<PCTuto>().finCDsort1 = Time.time;

        }
        otherspell = 0;
        numberSpellCast = 0;
        sphere.SetActive(false);
        gameObject.GetComponent<PCTuto>().setIsCasting(false);
        //CmdResetVarSpell(numeroJoueur);
        //CmdResetVarSpell(numeroautreJoueur);
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
