using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SubCamAnimIntro : NetworkBehaviour
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
    private GameObject CanvasJoueur;

    public GameObject mageAir;
    public GameObject mageEau;
    public GameObject mageFeu;

    // Use this for initialization
    void Start()
    {
        Twice = false;
        isIntroPassed = false;
        timer = 0.0f;
        skip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("EscapeAnimation") && Once)
        {
            skip = true;

        }
        if (skip)
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
                if (GameObject.Find("LOCAL Player").gameObject.GetComponent<Sorts_simple>().numeroJoueur == 0)
                {
                    CmdSwitchRespawnBegin();
                }
                CanvasJoueur.SetActive(true);
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
                CanvasJoueur = GameObject.Find("CanvasJ1(Clone)");
                CanvasJoueur.SetActive(false);
                col.GetComponent<PlayerController>().IsUnderCine = true;
                MainCamera = GameObject.Find("Main Camera");

                SubCamera.GetComponent<subCameraController>().changeMusic("intro");
                SubCamera.SetActive(true);
                timer = Time.time;

                //position initiale des mages
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.position = new Vector3(-176.29f, 0.5f, -438.625f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.position = new Vector3(-179.84f, 0.5f, -438.625f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.position = new Vector3(-183.72f, 0.5f, -438.625f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.rotation = new Quaternion(0, 0, 0, 0);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.rotation = new Quaternion(0, 0, 0, 0);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.rotation = new Quaternion(0, 0, 0, 0);


                Once = true;

            }

            if (Time.time - timer > 32.0f && !skip)
            {
                if (GameObject.Find("LOCAL Player").gameObject.GetComponent<Sorts_simple>().numeroJoueur == 0)
                {
                    CmdSwitchRespawnBegin();
                }
                CanvasJoueur.SetActive(true);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                SubCamera.SetActive(false);
                AmbiantSound.SetActive(true);
            }
            if (Time.time - timer > 34.0f && !skip)
            {
                Destroy(gameObject);
            }

        }
    }

    [Command]
    private void CmdSwitchRespawnBegin()
    {
        GameObject.Find("LOCAL Player").GetComponent<NetworkedPlayerScript>().RpcSwitchRespawnBegin();
    }


}
