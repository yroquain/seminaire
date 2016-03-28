using UnityEngine;
using System.Collections;

public class scriptAlmanach : MonoBehaviour {

    public GameObject menuPause;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))&& this.gameObject.GetComponentsInChildren<Transform>().Length > 0)
        {            
            this.gameObject.SetActive(false);
            menuPause.SetActive(true);
        }
	}
}
