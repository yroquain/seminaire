using UnityEngine;
using System.Collections;

public class Script1 : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    private float speed = 1f;
    public bool IsCompleted;

    private bool IsActivated;
	// Use this for initialization
	void Start () {
        IsActivated = false;
        IsCompleted = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsActivated)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, step);
        }
        if (transform.position.y < -0.40)
            IsCompleted = true;
    }
    void OnCollisionStay(Collision collision)
    {
        float pointCollision = transform.position.y;
        if (pointCollision+ 0.7 < collision.transform.position.y)
        {
            IsActivated = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        IsActivated = false; 
    }
}
