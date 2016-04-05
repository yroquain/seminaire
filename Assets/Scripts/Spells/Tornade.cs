using UnityEngine;
using System.Collections;

public class Tornade : MonoBehaviour {

    private float timetodie;
    private GameObject cameraa;
    private bool AmIHuman;
    public GameObject tornade;
    private int degat;
    private bool Once;
    // Use this for initialization
    void Start()
    {
        Once = true;
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
                    if (PlayerPrefs.GetFloat("TornadeEnflammee") == 0)
                    {
                        PlayerPrefs.SetFloat("TornadeEnflammee", 1);
                    }
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
        if(coll.gameObject.tag=="ennemi" && Once)
        {
            Once = false;
            //Reduire HP
            coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x, coll.gameObject.transform.position.y + 3, coll.gameObject.transform.position.z);
            if(tag== "TornadeEnflammee")
            {
                //Reduire HP encore plus
            }
        }
    }

}
