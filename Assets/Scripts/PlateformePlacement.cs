using UnityEngine;
using System.Collections;

public class PlateformePlacement : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker1;
    public Transform endMarker2;
    public GameObject Mage;
    private bool marker;
    private float speed = 3.0f;
    public bool IsActivated;
    private bool Firstiteration;
    private float timestart;
    private float timeend=15f;
    // Use this for initialization
    void Start()
    {
        IsActivated = false;
        marker = true;
        Firstiteration = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            if(Firstiteration)
            {
                Mage.GetComponent<PlayerController>().Animation();
                timestart = Time.time;
                Firstiteration = false;
            }
            float step = speed * Time.deltaTime;
            if (marker)
                transform.position = Vector3.MoveTowards(startMarker.position, endMarker1.position, step);
            else
                transform.position = Vector3.MoveTowards(startMarker.position, endMarker2.position, step);
            if (transform.position.y == endMarker1.transform.position.y)
                marker = false;
            if(Time.time>timestart+timeend)
            {
                Mage.GetComponent<PlayerController>().Animation();
                IsActivated = false;
            }
        }
    }
}
