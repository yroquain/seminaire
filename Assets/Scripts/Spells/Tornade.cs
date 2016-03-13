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
        if (coll.tag == "Mage_Feu")
        {
            if (coll.GetComponent<PlayerController>().IsImmolating)
            {
                this.GetComponent<ParticleSystem>().startColor = new Color(1, 0.5f, 0, 0f);
                this.GetComponent<ParticleSystem>().startColor = new Color(1, 0.25f, 0, .5f);
                tornade.GetComponent<ParticleSystem>().startLifetime = 4;
            }
        }
    }

}
