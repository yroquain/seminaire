using UnityEngine;
using System.Collections;

public class PreRain : MonoBehaviour {

    private GameObject cameraa;
    public GameObject cube;
    // Use this for initialization
    void Start () {

        cameraa = GameObject.FindWithTag("Mage_Eau").transform.Find("Main Camera").gameObject;
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
    }
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
    {
        Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
