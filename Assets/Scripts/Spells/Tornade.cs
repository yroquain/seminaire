using UnityEngine;
using System.Collections;

public class Tornade : MonoBehaviour {

    private float timetodie;
    private GameObject cameraa;
    private bool AmIHuman;
    public GameObject tornade;
    // Use this for initialization
    void Start()
    {
        cameraa = GameObject.Find("MageBourraqueInfernale");
        if (cameraa != null)
        {
            if (!cameraa.GetComponent<MageBourrasqueInfernale>().IsActivated)
            {
                AmIHuman = true;
            }
        }
        if (cameraa == null || AmIHuman)
        {
            cameraa = GameObject.FindWithTag("Mage_Air");
        }
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
            if (coll.name == "LOCAL Player" || coll.name == "Mage(Clone)")
            {
                if (coll.GetComponent<PlayerController>().IsImmolating)
                {
                    this.GetComponent<ParticleSystem>().startColor = tornade.GetComponent<ParticleSystem>().startColor;
                }
            }
            else if(coll.name=="MageTutorial")
            {

                if (coll.GetComponent<PCTuto>().IsImmolating)
                {
                    this.GetComponent<ParticleSystem>().startColor = tornade.GetComponent<ParticleSystem>().startColor;
                }
            }
        }
    }

}
