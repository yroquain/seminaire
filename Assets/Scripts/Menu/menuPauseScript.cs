using UnityEngine;
using System.Collections;

public class menuPauseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            this.gameObject.SetActive(false);
            GameObject.Find("LOCAL Player").GetComponent<PlayerController>().enabled = true;
        }
	}
}
