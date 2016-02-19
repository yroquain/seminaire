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
                    /*var overtime1 = particle1.GetComponent<ParticleSystem>().colorOverLifetime;
                    overtime1.enabled = true;
                    Gradient grad = new Gradient();
                    grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1,0.5f,0,1), 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                    overtime1.color= new ParticleSystem.MinMaxGradient(grad);
                    var overtime2 = particle2.GetComponent<ParticleSystem>().colorOverLifetime;
                    overtime2.enabled = true;
                    overtime2.color = new ParticleSystem.MinMaxGradient(grad);*/
                    this.tag = "MurIfrit";
                    IsIfrit = true;
                }
            }
            if (coll.tag == "ChocAquatique")
            {
                GetComponent<Collider>().enabled = false;
                particle1.SetActive(false);
                murG.SetActive(true);
                IsGivre = true;
                this.tag = "BarriereGivree";
            }
        }
    }
}
