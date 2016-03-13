using UnityEngine;
using System.Collections;

public class PreRain : MonoBehaviour {

    private GameObject Player;
    private GameObject cameraa;
    public GameObject cube;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("LOCAL Player");
        if (Player.GetComponent<PlayerController>().CDsort2 > 0)
        {
            cameraa = Player.transform.Find("Main Camera").gameObject;
        }
        else
        {
            Player = GameObject.FindWithTag("Mage(Clone)");
            cameraa = Player.transform.Find("Main Camera").gameObject;
        }
            
        GetComponent<Rigidbody>().velocity = cameraa.transform.forward * 10;
    }
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
    {
        Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
