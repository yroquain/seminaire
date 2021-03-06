﻿using UnityEngine;
using System.Collections;

public class SubCamAnimEnigm1 : MonoBehaviour
{
    private bool isIntroPassed;
    private bool Once;
    private float timer;
    public GameObject MainCamera;
    public GameObject SubCamera;
    public bool skip;
    public GameObject AmbiantSound;
    private GameObject CanvasJoueur;

    public GameObject mageAir;
    public GameObject mageEau;
    public GameObject mageFeu;

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
            skip = true;
            CanvasJoueur.SetActive(true);
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
                timer = Time.time;

                SubCamera.GetComponent<subCameraController>().changeMusic("enigm1");
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_air.transform.position = new Vector3(-176.29f, 0.5f, -365.91f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_eau.transform.position = new Vector3(-173.37f, 0.5f, -367.98f);
                GameObject.Find("networkManager").GetComponent<GameController>().Mage_offline_feu.transform.position = new Vector3(-164.5f, 0.5f, -370.6f);

                mageAir.GetComponent<MageCinematique>().rotateMage(1f);
                mageFeu.GetComponent<MageCinematique>().moveMage(0f, 1f, 1f);

                Once = true;

            }
            
                if (Time.time - timer > 29.4f && !skip)
                {
                    CanvasJoueur.SetActive(true);
                    GameObject.Find("LOCAL Player").GetComponent<PlayerController>().IsUnderCine = false;
                    SubCamera.SetActive(false);
                }
            if(Time.time - timer > 31.4f && !skip)
            {
                    Destroy(gameObject);

            }

        }
    }
}