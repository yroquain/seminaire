using UnityEngine;
using System.Collections;

public class Script1 : MonoBehaviour {

    public Transform startMarker;
    public Transform endMarker;
    private float speed = 1f;
    private float timer;
    public GameObject MainCamera;
    public GameObject SubCamera;
    public bool IsCompleted;
    private bool Once;
    private bool skip;
    private bool twice;
    private bool IsActivated;
    public GameObject Cube1;
    public GameObject Cube2;
    public GameObject Cube3;
    public GameObject Cube4;
    private bool thrice;
    // Use this for initialization
    void Start () {
        thrice = false;
        IsActivated = false;
        IsCompleted = false;
        Once = false;
        skip = false;
        twice = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (IsActivated)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(startMarker.position, endMarker.position, step);
        }
        if (transform.position.y < -0.40)
        {

            if (Input.GetButtonDown("EscapeAnimation") && Once)
            {
                skip = true;
                SubCamera.SetActive(false);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                if (Time.time - timer > 7f && thrice)
                {
                    Destroy(GameObject.Find("One shot audio"));
                }
                Cube3.SetActive(true);
                Cube4.SetActive(false);
                Destroy(gameObject);
            }
            IsCompleted = true;
            if (!Once)
            {
                Cube1.SetActive(false);
                Cube2.SetActive(true);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = true;
                MainCamera = GameObject.Find("Main Camera");
                SubCamera.SetActive(true);
                SubCamera.GetComponent<Animation>().enabled = false;
                SubCamera.GetComponent<Animator>().enabled = false;
                timer = Time.time;
                SubCamera.transform.position = new Vector3(-195f, 7.4f, -358.5f);
                SubCamera.transform.rotation = Quaternion.Euler(25, 90, 0);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.position = new Vector3(-176.29f, 0.5f, -366f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.position = new Vector3(-179.84f, 0.5f, -366f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.position = new Vector3(-146.7f, 0.5f, -329.09f);
                Once = true;

            }
            if(Time.time - timer > 7f && !thrice)
            {
                thrice = true;
                SubCamera.GetComponent<subCameraController>().changeMusic("Postenigme1");
                SubCamera.GetComponent<subCameraController>().playAnimation("Defaut");
            }
            if (Time.time - timer > 22.9f && !skip && !twice)
            {
                Cube4.SetActive(false);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                SubCamera.SetActive(false);
                twice = true;
            }
        }
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
