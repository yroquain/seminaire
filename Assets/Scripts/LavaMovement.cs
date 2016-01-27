using UnityEngine;
using System.Collections;

public class LavaMovement : MonoBehaviour {


    public Transform startMarker;
    public Transform endMarker;
    private float speed=5f;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, step);
        if (transform.position == endMarker.position)
        {
            transform.position = new Vector3(startMarker.position.x - 180, startMarker.position.y, startMarker.position.z);
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //if not fire type || !IsImmolating kill
        }
    }
}
