using UnityEngine;
using System.Collections;

public class Skeleton1 : MonoBehaviour {


    private float timeDying;
    private float timestanding;
    private bool Isstanding;
    private bool IsDying;
    public float diffQat;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(IsDying)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4, transform.position.z), Time.deltaTime/3);
        }
	    if(Time.time> timeDying+1.8f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 4.5f, transform.position.z), Time.deltaTime/4);
            if (IsDying)
            {
                transform.rotation = Quaternion.Euler(0.0f, -90.0f+ diffQat, 0.0f);
                IsDying = false;
                GetComponent<Animation>().Play("StandUp02");
                timestanding = Time.time;
                Isstanding = true;
            }
        }
        if(!IsDying && (!Isstanding || Time.time> timestanding+2))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 180.0f + diffQat, 0.0f), Time.deltaTime * 50f * 2);
            GetComponent<Animation>().Play("Idle");
            Isstanding = false;
        }
	}
    public void OnCollisionEnter(Collision Collide)
    {
        if(Collide.gameObject.name!="Cube" && !IsDying)
        {
            if ((transform.rotation.eulerAngles.y> 180.0f + diffQat-0.2f && transform.rotation.eulerAngles.y < 180.0f + diffQat + 0.2f ))
            {
                GetComponent<Animation>().Play("Death");
                IsDying = true;
                timeDying = Time.time;
            }
        }
    }
    public void OnTriggerEnter(Collider Collide)
    {
        if (Collide.gameObject.name != "Cube" && !IsDying)
        {
            if ((transform.rotation.eulerAngles.y > 180.0f + diffQat - 0.2f && transform.rotation.eulerAngles.y < 180.0f + diffQat + 0.2f))
            {
                GetComponent<Animation>().Play("Death");
                IsDying = true;
                timeDying = Time.time;
            }
        }
    }
}
