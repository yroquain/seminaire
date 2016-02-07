using UnityEngine;
using System.Collections;

public class TriggerInstensifes : MonoBehaviour {

    public GameObject[] g1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

    void OnTriggerEnter()
    {
        for (int i = 0; i < 9; i++)
        {
            g1[i].GetComponent<PlateformePlacement>().IsActivated = true;
        }
    }
}
