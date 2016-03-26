using UnityEngine;
using System.Collections;

public class PreRain : MonoBehaviour {
    
    private GameObject cameraa;
    public GameObject cube;
    // Use this for initialization
    void Start () {
        cameraa = GameObject.Find("LOCAL Player");
        if (cameraa != null)
        {
            if (cameraa.GetComponent<Sorts_simple>().IsUsingSpell)
            {
                GetComponent<Rigidbody>().velocity = cameraa.transform.Find("Main Camera").gameObject.transform.forward * 20;
            }
            else
            {
                cameraa = GameObject.Find("Mage(Clone)");
                GetComponent<Rigidbody>().velocity = cameraa.transform.Find("Main Camera").gameObject.transform.forward * 20;
            }
        }
        else
        {
            cameraa = GameObject.Find("MageTutorial");
            GetComponent<Rigidbody>().velocity = cameraa.transform.Find("Main Camera").gameObject.transform.forward * 20;
        }
    }
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
    {
        Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
