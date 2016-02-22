using UnityEngine;
using System.Collections;

public class playerChoixElement : MonoBehaviour {

    public string elementChoisis;
    public int numberPlayer;
    public bool ready;
    public GameObject prefabJoueur;

	// Use this for initialization
	void Start () {
        int nombreJoueur = GameObject.FindGameObjectsWithTag("Player").Length;
        if (nombreJoueur == 1)
        {
            numberPlayer = 1;
            elementChoisis = "Feu";
        }
        else if (nombreJoueur == 2)
        {
            numberPlayer = 2;
            elementChoisis = "Air";
        }
	}
	
	// Update is called once per frame
	void Update () {
        int nombreJoueur =GameObject.FindGameObjectsWithTag("Player").Length;
        if (nombreJoueur == 2)
        {
            
        }
	}
}
