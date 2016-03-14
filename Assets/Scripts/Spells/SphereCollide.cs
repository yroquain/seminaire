using UnityEngine;
using System.Collections;

public class SphereCollide : MonoBehaviour {

    public GameObject MyPlayer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter(Collider collide)
    {
        if(collide.gameObject.tag=="Mage_Feu" && collide.gameObject.GetComponent<Sorts_simple>().IsImmolating && collide.gameObject!=MyPlayer)
        {
            MyPlayer.GetComponent<Sorts_simple>().ImmolatingSpell = true;
        }
    }
}
