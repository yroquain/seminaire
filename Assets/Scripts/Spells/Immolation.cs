using UnityEngine;
using System.Collections;

public class Immolation : MonoBehaviour {

    private int Degat;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.tag=="ennemi")
        {
            //Reduire HP
        }
    }
    public void OnTriggerStay(Collider Coll)
    {
        if (Coll.gameObject.tag == "ennemi")
        {
            //Reduire HP
        }
    }
}
