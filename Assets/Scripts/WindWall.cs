using UnityEngine;
using System.Collections;

public class WindWall : MonoBehaviour {

    public GameObject particle2;
    public GameObject particle1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag=="Mage_Feu")
        {
            if (coll.GetComponent<PlayerController>().IsImmolating)
            {
                particle1.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, .08f);
                particle2.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, .08f);
                this.tag = "MurIfrit";
            }
        }
    }
}
