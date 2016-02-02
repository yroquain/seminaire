using UnityEngine;
using System.Collections;

public class Sorts_Eau : MonoBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;

    public GameObject trait;
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
            throw new System.Exception("not implemented yet");
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;


        }




    }
}
