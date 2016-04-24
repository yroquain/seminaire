using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {

    private bool[] verif;
    private int count;
    private bool IsActive;
    public GameObject wind;
	// Use this for initialization
	void Start () {
        wind.SetActive(false);
        IsActive = false;
        verif = new bool[3];
        verif[0] = false;
        verif[1] = false;
        verif[2] = false;
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!IsActive)
        {
            count = 0;
            GameObject Player = GameObject.FindWithTag("Mage_Feu");
            if (Player != null)
            {
                if (Player.transform.position.z > -11)
                {
                    verif[0] = true;
                }
                else
                {
                    verif[0] = false;
                }
            }
            else
            {
                verif[0] = false;
            }
            Player = GameObject.FindWithTag("Mage_Eau");
            if (Player != null)
            {
                if (Player.transform.position.z > -11)
                {
                    verif[1] = true;
                }
                else
                {
                    verif[1] = false;
                }
            }
            else
            {
                verif[1] = false;
            }
            Player = GameObject.FindWithTag("Mage_Air");
            if (Player != null)
            {
                if (Player.transform.position.z > -11)
                {
                    verif[2] = true;
                }
                else
                {
                    verif[2] = false;
                }
            }
            else
            {
                verif[2] = false;
            }
            for(int i=0; i<3;i++)
            {
                if(verif[i])
                {
                    count++;
                }
            }
            if(count==2)
            {
                wind.SetActive(true);
                IsActive = true;
            }
        }
	}
}
