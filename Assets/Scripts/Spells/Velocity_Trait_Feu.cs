using UnityEngine;
using System.Collections;

public class Velocity_Trait_Feu : MonoBehaviour {

    private GameObject cameraa;
    private bool AmIHuman;
    private float timetodie;
	// Use this for initialization
	void Start () {
        timetodie = Time.time;
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
    }
    void Update()
    {
        if(Time.time>timetodie+0.5f)
        {
            Destroy(gameObject);
        }
    }
}
