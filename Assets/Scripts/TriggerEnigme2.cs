using UnityEngine;
using System.Collections;

public class TriggerEnigme2 : MonoBehaviour {
    public Transform startMarker;
    public Transform endMarker1;
    public GameObject[] g1;
    private bool IsInitialised;
    private bool IsFirstIteration;
    private float speed = 100.0f;
    // Use this for initialization
    void Start () {
        IsInitialised = false;
        IsFirstIteration = true;
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        if (IsInitialised)
        {
            transform.rotation = Quaternion.RotateTowards(startMarker.rotation, endMarker1.rotation, step/3);
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker1.position, Time.deltaTime/4f);
            if (transform.position == endMarker1.position && IsFirstIteration)
            {
                for (int i = 0; i < 13; i++)
                {
                    g1[i].GetComponent<PlateformePlacement>().IsActivated = true;
                }
                IsFirstIteration = false;
            }
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Mage_Feu" || coll.gameObject.tag == "Mage_Eau" || coll.gameObject.tag == "Mage_Air" )
        {
            if(coll.gameObject.GetComponent<PlayerController>().IsAttacking)
            {
                IsInitialised = true;
            }
        }
    }
}
