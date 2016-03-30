using UnityEngine;
using System.Collections;

public class WallDestruction : MonoBehaviour {
    public Transform startMarker;
    public Transform endMarker1;
    private float speed = 3.0f;
    bool IsActivated;
    // Use this for initialization
    void Start () {
        IsActivated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsActivated)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker1.position, step);
        }
        if(transform.position.y<-10)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="ChocAquatique" || collision.gameObject.tag== "Obsidienne")
        {
            IsActivated = true;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "TraitFeu")
        {
            IsActivated = true;
        }
    }
}
