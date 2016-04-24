using UnityEngine;
using System.Collections;

public class SubCamAnimPreBoss : MonoBehaviour {

    private bool isIntroPassed;
    private bool Once;
    private float timer;
    public GameObject MainCamera;
    public GameObject SubCamera;
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
                col.GetComponent<PlayerController>().IsUnderCine = true;
                MainCamera = GameObject.Find("Main Camera");
                SubCamera.SetActive(true);
                SubCamera.GetComponent<Animation>().enabled = false;
                SubCamera.GetComponent<Animator>().enabled = false;
                timer = Time.time;
                SubCamera.GetComponent<subCameraController>().changeMusic("prepreBoss");
               // SubCamera.GetComponent<subCameraController>().playAnimation("Defaut");
                SubCamera.transform.position = new Vector3(-180.6f, 7.4f, -84.15f);
                SubCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.position = new Vector3(-176.29f, 0.5f, -68.1f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.position = new Vector3(-179.84f, 0.5f, -68.1f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.position = new Vector3(-183.72f, 0.5f, -68.1f);
                Once = true;

            }

            if (Time.time - timer > 26.7f && !skip)
            {
                CanvasJoueur.SetActive(true);
                col.GetComponent<PlayerController>().IsUnderCine = false;
                SubCamera.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
