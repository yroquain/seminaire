using UnityEngine;
using System.Collections;

public class Sorts_Air : MonoBehaviour {



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;

    public GameObject wind;

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
        //Bourrasque infernale
        if (numberSpell == 2)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Mage_Air");
            Vector3 position = new Vector3(player.transform.position.x + cameraa.transform.forward.x * 2,
                player.transform.position.y ,
                player.transform.position.z + cameraa.transform.forward.z * 2);
            Instantiate(wind, position, Quaternion.identity);
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;
            
        }
        //Mur d'Éole
        else if (numberSpell == 1)
        {
            throw new System.Exception("not implemented yet");
        }


    }
}
