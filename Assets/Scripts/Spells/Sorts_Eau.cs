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
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

    }


    public void CastSpell(int numberSpell)
    {
        //Choc aquatique

        if (numberSpell == 1)
        {
            Vector3 position = new Vector3(pos.position.x,
                pos.position.y,
                pos.position.z);
            Instantiate(trait, position, Quaternion.identity);
        }
        //Pluie divine
        else if (numberSpell == 2)
        {
            Vector3 position = new Vector3((transform.position.x+cameraa.transform.forward.x*2),
                transform.position.y+2,
                transform.position.z+ cameraa.transform.forward.z * 2);
            Instantiate(prerain, position, Quaternion.identity);
            //CmdPluieDivine();
        }
    }

    [Command]
    public void CmdPluieDivine()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcPluieDivine(this.gameObject);
    }
}
