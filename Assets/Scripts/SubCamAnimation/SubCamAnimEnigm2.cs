using UnityEngine;
using System.Collections;

public class SubCamAnimEnigm2 : MonoBehaviour {

    private bool isIntroPassed;
    private bool Once;
    private float timer;
    public GameObject MainCamera;
    public GameObject SubCamera;
    public GameObject AmbiantMusic;
    public GameObject CombatMusic;
    private GameObject CanvasJoueur;
    public bool skip;
    // Use this for initialization
    void Start()
    {
        isIntroPassed = false;
        timer = 0.0f;
        skip = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("EscapeAnimation") && Once)
        {
            CanvasJoueur.SetActive(true);
            skip = true;
            SubCamera.SetActive(false);
            GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
            Destroy(GameObject.Find("One shot audio"));
            Destroy(gameObject);
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
                AmbiantMusic.SetActive(true);
                CombatMusic.SetActive(false);
                col.GetComponent<PlayerController>().IsUnderCine = true;
                MainCamera = GameObject.Find("Main Camera");
                SubCamera.SetActive(true);
                timer = Time.time;
                SubCamera.GetComponent<subCameraController>().changeMusic("enigme2");
                SubCamera.transform.position = new Vector3(-190f, 6.96f, -224.23f);
                SubCamera.transform.rotation = Quaternion.Euler(0, 30, 0);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.position = new Vector3(-176.29f, 0.5f, -207.59f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.position = new Vector3(-179.84f, 0.5f, -207.59f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.position = new Vector3(-183.72f, 0.5f, -207.59f);
                Once = true;

            }

            if (Time.time - timer > 31.1f && !skip)
            {
                CanvasJoueur.SetActive(true);
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                SubCamera.SetActive(false);
            }
            if (Time.time - timer > 33.1f && !skip)
            {
                Destroy(gameObject);

            }
        }
    }
}
