using UnityEngine;
using System.Collections;

public class JetObsidienne : MonoBehaviour {

    private GameObject cameraa;
    private float timetodie;
    private int degat;
    // Use this for initialization
    void Start()
    {
        degat = 15;
        timetodie = Time.time;
        cameraa = GameObject.Find("MageTutorial");
        if (cameraa != null)
        {
            GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
        }
        else
        {
            cameraa = GameObject.Find("LOCAL Player");
            if (cameraa.GetComponent<Sorts_simple>().IsUsingSpell)
            {
                GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
            }
            else
            {
                cameraa = GameObject.Find("Mage(Clone)");
                GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
            }
        }
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    void Update()
    {
        if (Time.time > timetodie + 5f)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision Coll)
    {        
        Destroy(gameObject);
        if(Coll.gameObject.tag=="ennemi")
        {
            Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - degat;
        }
    }
}
