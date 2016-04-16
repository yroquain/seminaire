using UnityEngine;
using System.Collections;

public class Velocity_Trait_Feu : MonoBehaviour {

    private GameObject cameraa;
    private bool AmIHuman;
    private float timetodie;
    public int Degat;
	// Use this for initialization
	void Start () {
        timetodie = Time.time;
        Degat = 10;
        cameraa = GameObject.Find("MageChocAqua");
        if(cameraa!= null)
        {
            if (!cameraa.GetComponent<MageChocAqua>().IsActivated)
            {
                AmIHuman = true;
            }
        }
        if (cameraa == null || AmIHuman)
        {
            cameraa = GameObject.FindWithTag("Mage_Eau");
        }
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
        transform.rotation = cameraa.transform.rotation;

    }
    void Update()
    {
        if(Time.time>timetodie+0.5f)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision Coll)
    {
        if(Coll.gameObject.name!= "SteamSpray")
        {
            if(Coll.gameObject.tag=="ennemi")
            {
                Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
            }
            Destroy(gameObject);
        }
    }
}
