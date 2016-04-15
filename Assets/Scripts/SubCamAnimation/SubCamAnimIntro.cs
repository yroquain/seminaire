using UnityEngine;
using System.Collections;

public class SubCamAnimIntro : MonoBehaviour
{
    private bool isIntroPassed;
    private bool Once;
    private bool Twice;
    private float timer;
    private float timer2;
    public GameObject MainCamera;
    public GameObject SubCamera;
    public bool skip;
    public GameObject AmbiantSound;

    /* à ajouter aux autres triggers
    private bool isEnigm1EventPassed;
    private bool isEnigm2EventPassed;
    private bool isSkeletonEventPassed;
    private bool isPreBossEventPassed;
    */


    // Use this for initialization
    void Start()
    {
        Twice = false;
        isIntroPassed = false;
        timer = 0.0f;
        skip = false;
        /*
        isEnigm1EventPassed = false;
        isEnigm2EventPassed = false;
        isSkeletonEventPassed = false;
        isPreBossEventPassed = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
       // if(Input.GetButtonDown("Pause"))
        if (Input.GetButtonDown("EscapeAnimation") && Once)
        {
            skip = true;

        }
        if(skip)
        {
            if (!Twice)
            {
                timer2 = Time.time;
                Twice = true;
            }
            if (GameObject.Find("One shot audio") != null)
            {
                Destroy(GameObject.Find("One shot audio"));
            }
            if (Time.time > timer2 + 0.2f)
            {
                SubCamera.SetActive(false);
                AmbiantSound.SetActive(true);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                Destroy(gameObject);
            }
        }
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "LOCAL Player" || col.gameObject.name == "Mage(Clone)")
        {
            if (!Once)
            {
                col.GetComponent<PlayerController>().IsUnderCine = true;
                MainCamera = GameObject.Find("Main Camera");
                SubCamera.SetActive(true);
                timer = Time.time;
                SubCamera.GetComponent<subCameraController>().changeMusic("intro");
                SubCamera.GetComponent<subCameraController>().playAnimation("intro");
                Once = true;

            }

            if (Time.time - timer > 32.0f && !skip)
            {
                col.GetComponent<PlayerController>().IsUnderCine = false;
                SubCamera.SetActive(false);
                AmbiantSound.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}

