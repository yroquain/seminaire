using UnityEngine;
using System.Collections;

public class TimeRain : MonoBehaviour {

    private int Degat;
    private float tic;
    private float timebeforedeath;
	// Use this for initialization
	void Start ()
    {
        Degat = 2;
        tic = 0;
        timebeforedeath = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Time.time> timebeforedeath+7)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.tag=="ennemi" && tag != "FireRain")
        {
            Coll.gameObject.GetComponent<SkeletonController>().speed=5f;
        }
        if (Coll.gameObject.tag == "ennemi" && tag == "FireRain" && Time.time > tic + 0.5f)
        {
            Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
            tic = Time.time;
        }
        if (Coll.gameObject.name== "Trait_Feu(Clone)")
        {
            Coll.gameObject.GetComponent<Velocity_Trait_Feu>().Degat = 15;
        }
    }
    public void OnTriggerStay(Collider Coll)
    {

        if (Coll.gameObject.tag == "ennemi" && tag!="FireRain")
        {
            Coll.gameObject.GetComponent<SkeletonController>().speed = 5f;
        }
        if (Coll.gameObject.tag == "ennemi" && tag == "FireRain" && Time.time > tic + 0.5f)
        {
                Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
                tic = Time.time;
        }
        if (Coll.gameObject.tag == "ennemi" && tag == "Typhon")
        {
            float x = Random.Range(0, 1);
            float y = Random.Range(0, 1);
            float z = Random.Range(0, 1);
            Coll.gameObject.transform.position = new Vector3(Coll.gameObject.transform.position.x+x, 
                Coll.gameObject.transform.position.y+y, 
                Coll.gameObject.transform.position.z+z);
        }
        if (Coll.gameObject.name == "Trait_Feu(Clone)")
        {
            Coll.gameObject.GetComponent<Velocity_Trait_Feu>().Degat = 15;
        }
    }
    public void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "ennemi" && tag != "FireRain")
        {
            coll.gameObject.GetComponent<SkeletonController>().speed = 10f;
        }
        if (coll.gameObject.name == "Trait_Feu(Clone)")
        {
            coll.gameObject.GetComponent<Velocity_Trait_Feu>().Degat = 10;
        }
    }
}
