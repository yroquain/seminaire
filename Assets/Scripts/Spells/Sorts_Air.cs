using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_Air : NetworkBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;

    private bool castMurEole;
    private bool castBourrasqueInfernale;
    private float timeCast;
    private float timeCastMax = 2f;

    public GameObject wind;
    public GameObject mur;
    private GameObject muractif;
    private bool Isactivated;

    #region Initialisation
    void Start()
    {
        this.damage = 10.0f;
        this.manaCost = 10.0f;
        this.CD = 10.0f;
        this.range = 100.0f;
        Isactivated = false;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        if (castMurEole || castBourrasqueInfernale)
        {
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == true)
            {
                timeCast += Time.deltaTime;
            }
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == false || timeCast > timeCastMax)
            {
                if (castMurEole)
                {
                    CmdMurEole();
                    castMurEole = false;
                }
                if (castBourrasqueInfernale)
                {
                    CmdBourrasqueInfernale();
                    castBourrasqueInfernale = false;
                }


                timeCast = 0f;
                this.gameObject.GetComponent<PlayerController>().setIsCasting(false);
            }

        }
    }


    public void CastSpell(int numberSpell)
    {
        //Bourrasque infernale
        if (numberSpell == 1)
        {
            /*Vector3 position = new Vector3(transform.position.x + cameraa.transform.forward.x * 2,
                transform.position.y ,
                transform.position.z + cameraa.transform.forward.z * 2);
            Instantiate(wind, position, Quaternion.identity);*/

            castMurEole = true;
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;
            
        }
        //Mur d'Éole
        else if (numberSpell == 2)
        {
            /*if (!Isactivated)
            {
                muractif = GameObject.Instantiate(mur);
                muractif.transform.position = new Vector3(transform.position.x + cameraa.transform.forward.x * 2,
                    transform.position.y,
                    transform.position.z + cameraa.transform.forward.z * 2);
                muractif.transform.rotation = transform.rotation;

            }
            else
            {
                Destroy(muractif.gameObject);
            }
            Isactivated = !Isactivated;*/
            castBourrasqueInfernale = true;
        }


    }

    

    [Command]
    public void CmdMurEole()
    {
       this.gameObject.GetComponent<NetworkedPlayerScript>().RpcMurEole(this.gameObject);
    }
    [Command]
    public void CmdBourrasqueInfernale()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcBourrasqueInfernale(this.gameObject);
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
