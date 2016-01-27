using UnityEngine;
using System.Collections;

public class Plateforme1 : MonoBehaviour {


    public Transform startMarker;
    public Transform endMarker;
    public GameObject Trigger;
    private float speed = 1f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Trigger.GetComponent<Script1>().IsCompleted)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, step);
        }
    }
}
