using UnityEngine;
using System.Collections;

public class Plateforme1 : MonoBehaviour {


    public Transform startMarker;
    public Transform endMarker;
    public GameObject Trigger;
    private float speed = 1f;
    public Material texture_depart;
    public Material texture_finale;
    public bool IsDeadly;
    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material = texture_depart;
        IsDeadly = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Trigger.GetComponent<Script1>().IsCompleted)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, step);
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag=="PluieDivine" || coll.gameObject.tag == "BourrasqueInfernale")
        {
            GetComponent<Renderer>().material = texture_finale;
            IsDeadly = false;
        }
    }
}
