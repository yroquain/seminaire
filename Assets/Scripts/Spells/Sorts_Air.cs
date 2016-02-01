using UnityEngine;
using System.Collections;

public class Sorts_Air : MonoBehaviour {



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;

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
    }


    public void CastSpell(int numberSpell)
    {
        //Bourrasque infernale
        if (numberSpell == 2)
        {
            Vector3 position = new Vector3(transform.position.x + cameraa.transform.forward.x * 2,
                transform.position.y ,
                transform.position.z + cameraa.transform.forward.z * 2);
            Instantiate(wind, position, Quaternion.identity);
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;
            
        }
        //Mur d'Éole
        else if (numberSpell == 1)
        {
            if (!Isactivated)
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
            Isactivated = !Isactivated;
        }


    }
}
