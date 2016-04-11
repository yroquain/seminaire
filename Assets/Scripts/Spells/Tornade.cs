using UnityEngine;
using System.Collections;

public class Tornade : MonoBehaviour
{

    private float timetodie;
    private GameObject cameraa;
    private bool AmIHuman;
    public GameObject tornade;
    private int degat;
    private bool Once;
    // Use this for initialization
    void Start()
    {
        degat = 10;
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
        if (Time.time > timetodie + 3)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Mage_Feu" && this.gameObject.name != "TornadeEnflammee(Clone)")
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
            else if (coll.name == "MageTutorial")
            {

                if (coll.GetComponent<PCTuto>().IsImmolating)
                {
                    this.GetComponent<ParticleSystem>().startColor = tornade.GetComponent<ParticleSystem>().startColor;
                }
            }
        }
        if (coll.gameObject.tag == "ennemi")
        {
            coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - degat;
            coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x, 3, coll.gameObject.transform.position.z);
            if (tag == "TornadeEnflammee")
            {
                coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - 2;
            }
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "ennemi")
        {
            coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x, 3, coll.gameObject.transform.position.z);

        }

    }
}
