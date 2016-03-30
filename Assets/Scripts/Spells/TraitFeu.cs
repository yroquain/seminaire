using UnityEngine;
using System.Collections;

public class TraitFeu : MonoBehaviour
{
    private GameObject cameraa;
    private GameObject Player;
    public GameObject fleche;
    private bool AmIHuman;
    private float todie;
    private int degat;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("MageTraitdeFeu");
        if (Player != null)
        {
            if (!Player.GetComponent<MageTraitdeFeu>().IsActivated)
            {
                AmIHuman = true;
            }
            else
            {
                cameraa = Player.transform.Find("FalseCamera").gameObject;
            }
        }
        if (Player == null || AmIHuman)
        {
            Player = GameObject.FindWithTag("Mage_Feu");
            if (Player == null)
            {
                Player = GameObject.FindWithTag("Mage_Eau");
            }
            cameraa = Player.transform.Find("Main Camera").gameObject;
        }
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
        todie = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > todie + 10)
        {

            Destroy(GameObject.Find("Firebolt(Clone)"));
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.name != "Fireball(Clone)")
        {
            if (Coll.gameObject.tag != "Mage_Feu" && Coll.gameObject.tag != "Mage_Eau" && Coll.gameObject.tag != "Mage_Air" && Coll.gameObject.name != "FireboltCollider" && Coll.gameObject.name != "FireballCollider" && Coll.gameObject.name != "Trigger1C" && Coll.gameObject.name != "Trigger2C" && Coll.gameObject.name != "MageTutorial" && Coll.gameObject.name != "MagePluieDivine" && Coll.gameObject.name != "MageChocAqua" && Coll.gameObject.name != "MageBourraqueInfernale" && Coll.gameObject.name != "MageTraitdeFeu" && Coll.gameObject.name != "Giboule(Clone)")
            {

                if (Coll.gameObject.tag == "ennemi")
                {
                    //Reduire HP
                }
                if (Coll.tag == "MurEole")
                {
                    GameObject FlecheMortelle = (GameObject)Instantiate(fleche, this.transform.position, Quaternion.Euler(cameraa.transform.rotation.x / 3.14f * 360 + 270, cameraa.transform.rotation.y / 3.14f * 360, cameraa.transform.rotation.z / 3.14f * 360));
                    FlecheMortelle.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
                }
                Destroy(GameObject.Find("Firebolt(Clone)"));
                Destroy(GameObject.Find("Fireball(Clone)"));
                Destroy(gameObject);
            }
        }
    }
}
