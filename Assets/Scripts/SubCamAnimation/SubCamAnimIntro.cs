using UnityEngine;
using System.Collections;

public class SubCamAnimIntro : MonoBehaviour
{
    private bool isIntroPassed;
    private bool Once;
    private float timer;
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
        if (Input.GetButtonDown("EscapeAnimation"))
        {
            skip = true;
            SubCamera.SetActive(false);
            Destroy(gameObject);
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

