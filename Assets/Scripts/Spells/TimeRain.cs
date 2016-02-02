using UnityEngine;
using System.Collections;

public class TimeRain : MonoBehaviour {


    private float timebeforedeath;
	// Use this for initialization
	void Start () {
        timebeforedeath = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Time.time> timebeforedeath+7)
        {
            Destroy(gameObject);
        }
	}
}
