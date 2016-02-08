using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_Eau : NetworkBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;

    private bool castPluieDivine;
    private bool castChocAquatique;
    private float timeCast;
    private float timeCastMax = 2f;


    public GameObject trait;
    public GameObject prerain;
    public Transform pos;

    #region Initialisation
    void Start()
    {
        this.damage = 10.0f;
        this.manaCost = 10.0f;
        this.CD = 10.0f;
        this.range = 100.0f;
        timeCast = 0f;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        if (castChocAquatique || castPluieDivine)
        {
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == true)
            {
                timeCast += Time.deltaTime;
            }
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == false || timeCast > timeCastMax)
            {
                if (castChocAquatique)
                {
                    CmdChocAquatique();
                    castChocAquatique = false;
                }
                if (castPluieDivine)
                {
                    CmdPluieDivine();
                    castPluieDivine = false;
                }


                timeCast = 0f;
                this.gameObject.GetComponent<PlayerController>().setIsCasting(false);
            }
           
        }
    }


    public void CastSpell(int numberSpell)
    {
        //Choc aquatique

        if (numberSpell == 1)
        {
           /* Vector3 position = new Vector3(pos.position.x,
                pos.position.y,
                pos.position.z);
            Instantiate(trait, position, Quaternion.identity);*/
            castChocAquatique = true;
            
            
        }
        //Pluie divine
        else if (numberSpell == 2)
        {
           /* Vector3 position = new Vector3((transform.position.x+cameraa.transform.forward.x*2),
                transform.position.y+2,
                transform.position.z+ cameraa.transform.forward.z * 2);
            Instantiate(prerain, position, Quaternion.identity);*/
            castPluieDivine = true;
        }
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
}
