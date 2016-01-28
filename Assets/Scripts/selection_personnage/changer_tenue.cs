using UnityEngine;
using System.Collections;

public class changer_tenue : MonoBehaviour {

	// Use this for initialization
    public Material texture_air;
    public Material texture_eau;
    public Material texture_feu;

    private int numeroTenue=3;

	void Start () {
        changerTenue();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("c")){
            Debug.Log("coucou");
            changerTenue();
        }
	}

    public void changerTenue()
    {
        if (numeroTenue==3)
        {
            this.gameObject.transform.Find("Mage").GetComponent<Renderer>().material = texture_air;
            numeroTenue = 1;
        }
        else if (numeroTenue == 1)
        {
            this.gameObject.transform.Find("Mage").GetComponent<Renderer>().material = texture_eau;
            numeroTenue++;
        }
        else if (numeroTenue == 2)
        {
            this.gameObject.transform.Find("Mage").GetComponent<Renderer>().material = texture_feu;
            numeroTenue++;
        }
         
            
    }
}
