using UnityEngine;
using System.Collections;

public class Sorts_Air : MonoBehaviour {



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject camera;

    public Transform trait;

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
        if (numberSpell == 1) //trait de feu
        {
            GameObject player = GameObject.FindGameObjectWithTag("Mage_Air");
            Vector3 position = new Vector3(player.transform.position.x + camera.transform.forward.x * 2,
                player.transform.position.y + 2,
                player.transform.position.z + camera.transform.forward.z * 2);
            Instantiate(trait, position, Quaternion.identity);
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;


            Debug.Log("trait de feu tiré");
        }
        else if (numberSpell == 2) //immolation
        {
            throw new System.Exception("not implemented yet");
        }


    }
}
