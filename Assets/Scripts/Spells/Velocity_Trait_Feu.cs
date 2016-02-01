using UnityEngine;
using System.Collections;

public class Velocity_Trait_Feu : MonoBehaviour {

    private GameObject cameraa;
    private float timetodie;
	// Use this for initialization
	void Start () {
        timetodie = Time.time;
        cameraa = GameObject.FindWithTag("Mage_Eau");
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
    }
    void Update()
    {
        if(Time.time>timetodie+0.5f)
        {
            Destroy(gameObject);
        }
    }
}
