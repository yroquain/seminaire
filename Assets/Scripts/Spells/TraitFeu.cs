using UnityEngine;
using System.Collections;

public class TraitFeu : MonoBehaviour {
    private GameObject cameraa;
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
        if (Coll.gameObject.tag != "Mage_Feu" && Coll.gameObject.tag != "Mage_Eau" && Coll.gameObject.tag != "Mage_Air" && Coll.gameObject.name!="FireboltCollider")
        {
            Debug.Log(Coll.gameObject.name);

            Destroy(GameObject.Find("Firebolt(Clone)"));
            Destroy(gameObject);
        }
    }
}
