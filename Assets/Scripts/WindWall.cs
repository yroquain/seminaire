using UnityEngine;
using System.Collections;

public class WindWall : MonoBehaviour {

    public GameObject particle2;
    public GameObject particle1;
    public GameObject murG;
    private bool IsIfrit;
    private bool IsGivre;
    // Use this for initialization
    void Start () {
        IsIfrit = false;
        IsGivre = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (!IsIfrit && !IsGivre)
        {
            if (coll.tag == "Mage_Feu")
            {
                if (coll.GetComponent<PlayerController>().IsImmolating)
                {
                    particle1.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, .08f);
                    particle2.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, .08f);
                    this.tag = "MurIfrit";
                    IsIfrit = true;
                }
            }
            if (coll.tag == "ChocAquatique")
            {
                particle1.SetActive(false);
                murG.SetActive(true);
                IsGivre = true;
                this.tag = "BarriereGivree";
            }
        }
    }
}
