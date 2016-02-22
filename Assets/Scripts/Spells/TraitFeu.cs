using UnityEngine;
using System.Collections;

public class TraitFeu : MonoBehaviour {
    private GameObject cameraa;
    public GameObject fleche;
    private float todie;
    // Use this for initialization
    void Start () {
        cameraa = GameObject.FindWithTag("Mage_Feu").transform.Find("Main Camera").gameObject;
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
        todie = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time> todie+10)
        {

            Destroy(GameObject.Find("Firebolt(Clone)"));
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.tag != "Mage_Feu" && Coll.gameObject.tag != "Mage_Eau" && Coll.gameObject.tag != "Mage_Air" && Coll.gameObject.name!="FireboltCollider" && Coll.gameObject.name !="Trigger1C" && Coll.gameObject.name !="Trigger2C")
        {
            if(Coll.tag=="MurEole")
            {
                GameObject FlecheMortelle = (GameObject)Instantiate(fleche, this.transform.position, Quaternion.Euler(cameraa.transform.rotation.x / 3.14f * 360+270, cameraa.transform.rotation.y / 3.14f * 360, cameraa.transform.rotation.z / 3.14f * 360));
                FlecheMortelle.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            }
            Destroy(GameObject.Find("Firebolt(Clone)"));
            Destroy(gameObject);
        }
    }
}
