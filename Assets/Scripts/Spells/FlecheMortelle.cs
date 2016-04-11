using UnityEngine;
using System.Collections;

public class FlecheMortelle : MonoBehaviour
{
    private int Degat;
    private float todie;
    // Use this for initialization
    void Start()
    {
        todie = Time.time;
        Degat = 15;
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
        if (Coll.gameObject.tag != "Mage_Feu" && Coll.gameObject.tag != "Mage_Eau" && Coll.gameObject.tag != "Mage_Air" && Coll.gameObject.name != "FireboltCollider" && Coll.gameObject.name != "FireballCollider" && Coll.gameObject.name != "Trigger1C" && Coll.gameObject.name != "Trigger2C" && Coll.gameObject.name != "MageTutorial" && Coll.gameObject.name != "MagePluieDivine" && Coll.gameObject.name != "MageChocAqua" && Coll.gameObject.name != "MageBourraqueInfernale" && Coll.gameObject.name != "MageTraitdeFeu" && Coll.gameObject.name != "Giboule(Clone)" && Coll.tag != "MurEole")
        {
            if(Coll.gameObject.tag=="ennemi")
            {
                Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton = Coll.gameObject.GetComponent<SkeletonController>().hpSkeleton - Degat;
            }
            Destroy(gameObject);
        }
    }
}
