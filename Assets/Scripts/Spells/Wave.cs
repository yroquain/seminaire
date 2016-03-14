using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject Player = GameObject.Find("LOCAL Player");
        if (Player != null)
        {
            if (Player.GetComponent<PlayerController>().CDsort2 == 0)
            {
                Player = GameObject.FindWithTag("Mage_Eau");
            }
        }
        else
        {
            Player = GameObject.Find("Mage(Clone)");
        }
        this.transform.rotation = Player.transform.rotation;

    }
	
}
