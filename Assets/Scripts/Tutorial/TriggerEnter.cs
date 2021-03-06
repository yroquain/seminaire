﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerEnter : MonoBehaviour
{


    //Sprite HUD
    public Sprite sort1Image;
    public Sprite sort2Image;
    public Sprite mageImage;

    public GameObject Sort1;
    public GameObject Sort2;
    public GameObject Image;
    public GameObject TextSort1;
    public GameObject TextSort2;

    public Material mage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "MageTutorial")
        {
            if (collide.gameObject.GetComponent<PCTuto>().IsImmolating)
            {
                collide.gameObject.GetComponent<PCTuto>().IsImmolating = !collide.gameObject.GetComponent<PCTuto>().IsImmolating;
                collide.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                collide.gameObject.GetComponent<PCTuto>().Immo.SetActive(false);
                collide.gameObject.GetComponent<SortSimpleTuto>().IsImmolating = false;
            }
            if (collide.gameObject.GetComponent<PCTuto>().IsEole)
            {
                Destroy(collide.gameObject.GetComponent<SortSimpleTuto>().getMurActif().gameObject);
                collide.gameObject.GetComponent<SortSimpleTuto>().setIsActivated(!collide.gameObject.GetComponent<SortSimpleTuto>().getIsActivated());
                collide.gameObject.GetComponent<SortSimpleTuto>().IsEole = false;
                collide.gameObject.GetComponent<PCTuto>().IsEole = false;
            }
            if (collide.gameObject.transform.position.x < -34)
            {
                collide.gameObject.tag = "Mage_Feu";
                TextSort1.GetComponent<Text>().text = "50";
                TextSort2.GetComponent<Text>().text = "10";
            }
            else if (collide.gameObject.transform.position.x > -30)
            {
                collide.gameObject.tag = "Mage_Air";
                TextSort1.GetComponent<Text>().text = "15";
                TextSort2.GetComponent<Text>().text = "30";
            }
            else
            {
                collide.gameObject.tag = "Mage_Eau";
                TextSort1.GetComponent<Text>().text = "30";
                TextSort2.GetComponent<Text>().text = "50";
            }
            collide.gameObject.transform.Find("Mage").GetComponent<Renderer>().material = mage;
            Sort1.GetComponent<Image>().sprite = sort1Image;
            Sort2.GetComponent<Image>().sprite = sort2Image;
            Image.GetComponent<Image>().sprite = mageImage;
        }
    }
}
