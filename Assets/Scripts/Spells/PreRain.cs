using UnityEngine;
using System.Collections;

public class PreRain : MonoBehaviour {
    
    private GameObject cameraa;
    private bool AmIHuman;
    public GameObject cube;
    // Use this for initialization
    void Start () {
        cameraa = GameObject.Find("MagePluieDivine");
        if (cameraa != null)
        {
            if (!cameraa.GetComponent<MagePluieDivine>().IsActivated)
            {
                AmIHuman = true;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = cameraa.transform.Find("FalseCamera").gameObject.transform.forward * 20;
            }
        }
        if (cameraa == null || AmIHuman)
        {
            cameraa = GameObject.Find("MageTutorial");
            if (cameraa != null)
            {
                GetComponent<Rigidbody>().velocity = cameraa.transform.Find("Main Camera").gameObject.transform.forward * 20;
            }
            else
            {
                cameraa = GameObject.Find("LOCAL Player");
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
        }
    }
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
    {
        Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
