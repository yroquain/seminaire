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
                    particle1.GetComponent<ParticleSystem>().startColor = new Color(1, 0.5f, 0, 0f);
                    particle2.GetComponent<ParticleSystem>().startColor = new Color(1, 0.25f, 0, .5f);
                    particle2.GetComponent<ParticleSystem>().startLifetime = 4;
                    this.tag = "MurIfrit";
                    IsIfrit = true;
                    if (PlayerPrefs.GetFloat("MurIfrit") == 0)
                    {
                        PlayerPrefs.SetFloat("MurIfrit", 1);
                    }
                }
            }
            if (coll.tag == "ChocAquatique")
            {
                GetComponent<Collider>().enabled = false;
                particle1.SetActive(false);
                murG.SetActive(true);
                IsGivre = true;
                this.tag = "BarriereGivree";
                if (PlayerPrefs.GetFloat("BarriereGivree") == 0)
                {
                    PlayerPrefs.SetFloat("BarriereGivree", 1);
                }
            }
        }
        if(IsIfrit && coll.gameObject.tag=="ennemi")
        {
            //Reduire HP
        }
    }
    public void OnTriggerStay(Collider coll)
    {
        if (IsIfrit && coll.gameObject.tag == "ennemi")
        {
            //Reduire HP
        }
    }
}
