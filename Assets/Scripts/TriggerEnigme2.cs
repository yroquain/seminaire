using UnityEngine;
using System.Collections;

public class TriggerEnigme2 : MonoBehaviour
{
    public Transform currentMarker;
    public Transform startMarker;
    public Transform endMarker1;
    public GameObject[] g1;
    public GameObject TE2;
    private float activation;
    private bool IsInitialised;
    private bool IsFirstIteration;
    private bool IsFirstIteration2;
    private bool IsOver;
    public bool IsReady;
    private bool IsGettingBack;
    private bool IsTriggered;
    private float speed = 100.0f;
    // Use this for initialization
    void Start () {
        IsInitialised = false;
        IsFirstIteration = true;
        IsFirstIteration2 = true;
        IsTriggered = false;
        IsGettingBack = false;
        IsReady = false;
        IsOver = false;
    }
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        if (!IsOver)
        {
            if (IsInitialised)
            {
                transform.rotation = Quaternion.RotateTowards(currentMarker.rotation, endMarker1.rotation, step / 3);
                transform.position = Vector3.MoveTowards(currentMarker.position, endMarker1.position, Time.deltaTime / 4f);
                if (transform.position == endMarker1.position)
                {
                    if (IsFirstIteration)
                    {
                        IsReady = true;
                        activation = Time.time;
                        IsFirstIteration = false;
                    }
                    if (TE2.GetComponent<TriggerEnigme2>().IsReady && IsFirstIteration2 && Time.time <= activation + 1 && IsReady)
                    {
                        for (int i = 0; i < 13; i++)
                        {
                            g1[i].GetComponent<PlateformePlacement>().IsActivated = true;
                        }
                        IsFirstIteration2 = false;
                        IsOver = true;
                    }
                    if (Time.time > activation + 1)
                    {
                        IsReady = false;
                        IsInitialised = false;
                        IsGettingBack = true;
                        IsFirstIteration2 = true;
                        IsFirstIteration = true;
                    }
                }
            }
            if (IsGettingBack)
            {
                transform.rotation = Quaternion.RotateTowards(currentMarker.rotation, startMarker.rotation, step / 3);
                transform.position = Vector3.MoveTowards(currentMarker.position, startMarker.position, Time.deltaTime / 4f);
                if (transform.position == startMarker.position)
                {
                    IsGettingBack = false;
                    IsTriggered = false;
                }
            }
        }
    }
    void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Mage_Feu" || coll.gameObject.tag == "Mage_Eau" || coll.gameObject.tag == "Mage_Air" )
        {
            if(coll.gameObject.GetComponent<PlayerController>().IsAttacking && !IsTriggered)
            {
                IsInitialised = true;
                IsTriggered = true;
            }
        }
    }
}
