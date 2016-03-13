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
                Player = GameObject.Find("Mage_Eau");
            }
        }
        else
        {
            Player = GameObject.FindWithTag("Mage(Clone)");
        }
        transform.rotation = Player.transform.rotation;

    }
	
}
