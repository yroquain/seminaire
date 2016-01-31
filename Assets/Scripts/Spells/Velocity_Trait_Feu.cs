using UnityEngine;
using System.Collections;

public class Velocity_Trait_Feu : MonoBehaviour {

    private GameObject cameraa;
	// Use this for initialization
	void Start () {
        cameraa = GameObject.Find("Main Camera");
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
    }
}
