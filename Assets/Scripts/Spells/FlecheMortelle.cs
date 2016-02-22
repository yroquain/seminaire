using UnityEngine;
using System.Collections;

public class FlecheMortelle : MonoBehaviour
{

    private float todie;
    // Use this for initialization
    void Start()
    {
        todie = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > todie + 10)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider Coll)
    {
        if (Coll.gameObject.tag != "Mage_Feu" && Coll.gameObject.tag != "Mage_Eau" && Coll.gameObject.tag != "Mage_Air" && Coll.gameObject.name != "FireboltCollider" && Coll.gameObject.name != "Trigger1C" && Coll.gameObject.name != "Trigger2C" && Coll.gameObject.tag!="MurEole")
        {
            Destroy(gameObject);
        }
    }
}
