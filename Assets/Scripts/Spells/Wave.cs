using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject cameraa = GameObject.Find("MageTutorial");
        if (cameraa != null)
        {
            this.transform.rotation = cameraa.transform.rotation;
        }
        else
        {
            cameraa = GameObject.Find("LOCAL Player");
            if (cameraa.GetComponent<Sorts_simple>().IsUsingSpell)
            {
                this.transform.rotation = cameraa.transform.rotation;
            }
            else
            {
                cameraa = GameObject.Find("Mage(Clone)");
                this.transform.rotation = cameraa.transform.rotation;
            }
        }
        

    }
	
}
