using UnityEngine;
using System.Collections;

public class Immolation : MonoBehaviour {

    private int Degat;
    private float tic;
	// Use this for initialization
	void Start () {
        Degat = 2;
        tic = 0;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.tag=="ennemi" && Time.time> tic+0.5f)
        {
            Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
            tic = Time.time;
        }
    }
    public void OnTriggerStay(Collider Coll)
    {
        if (Coll.gameObject.tag == "ennemi" && Time.time > tic + 0.5f)
        {
            Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
            tic = Time.time;
        }
    }
}
