using UnityEngine;
using System.Collections;

public class scriptPillier : MonoBehaviour {

    private GameObject gameController;
	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("networkManager").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Mage_Air" || other.gameObject.tag == "Mage_Eau" || other.gameObject.tag == "Mage_Feu")
        {
            if (gameController.GetComponent<GameController>().getIsAttacking(other.gameObject.GetComponent<ManagementHpMana>().numeroJoueur))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
