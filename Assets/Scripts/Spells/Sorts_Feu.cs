using UnityEngine;
using System.Collections;

public class Sorts_Feu : MonoBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public string element;
    public float range; //définit la portée de l'attaque

    public Transform trait;

    #region Initialisation
    void Start()
    {
        this.damage = 10.0f;
        this.manaCost = 10.0f;
        this.CD = 10.0f;
        this.element = "Water";
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
            Vector3 position = new Vector3(0, 0, 0);
            GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
            Transform newTrait = (Transform)Instantiate(trait, player.transform.position, Quaternion.identity);
            newTrait.transform.Translate(Camera.main.transform.forward * Time.deltaTime);

            Debug.Log("trait de feu tiré");
        }
        else if (numberSpell == 2) //immolation
        {
            throw new System.Exception("not implemented yet");
        }


    }

}
