using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class triggerEnigme1 : NetworkBehaviour {

    public GameObject spawnPrecedent1;
    public GameObject spawnPrecedent2;
    public GameObject newRespawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mage_Feu" || other.tag == "Mage_Eau" || other.tag == "Mage_Air")
        {
            CmdSwitchRespawn(this.gameObject);
        }
   }


    [Command]
    private void CmdSwitchRespawn(GameObject myTrigger)
    {
        GameObject.Find("LOCAL Player").GetComponent<NetworkedPlayerScript>().RpcSwitchRespawn(myTrigger);
    }
}
