using UnityEngine;
using System.Collections;

public class JetObsidienne : MonoBehaviour {

    private GameObject cameraa;
    private float timetodie;
    // Use this for initialization
    void Start()
    {
        timetodie = Time.time;
        cameraa = GameObject.FindWithTag("Mage_Eau");
        if (cameraa.GetComponent<PlayerController>().CDsort1 > 0)
        {
            GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
        }
        else
        {
            cameraa = GameObject.FindWithTag("Mage_Feu");
            GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 20;
        }
        gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    void Update()
    {
        if (Time.time > timetodie + 5f)
        {
            Destroy(gameObject);
        }
    }
}
