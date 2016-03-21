using UnityEngine;
using System.Collections;

public class Tornade : MonoBehaviour {

    private float timetodie;
    private GameObject cameraa;
    public GameObject tornade;
    // Use this for initialization
    void Start()
    {
        cameraa = GameObject.FindWithTag("Mage_Air");
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
        timetodie = Time.time;
    }

    void Update()
    {
        if(Time.time>timetodie+3)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Mage_Feu" && this.gameObject.name!= "TornadeEnflammee(Clone)")
        {
            if (coll.GetComponent<PlayerController>().IsImmolating)
            {
                this.GetComponent<ParticleSystem>().startColor = tornade.GetComponent<ParticleSystem>().startColor;
            }
        }
    }

}
