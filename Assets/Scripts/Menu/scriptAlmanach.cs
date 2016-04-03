using UnityEngine;
using System.Collections;

public class scriptAlmanach : MonoBehaviour {

    public GameObject menuPause;
    private int numeroPage;
    public GameObject[] listePage;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        numeroPage = 0;
        listePage[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))&& this.gameObject.GetComponentsInChildren<Transform>().Length > 0)
        {            
            this.gameObject.SetActive(false);
            menuPause.SetActive(true);
        }

        if (Input.GetAxis("Horizontal") > 0 && ((Time.time - timer) > 0.3f))
        {
            listePage[numeroPage].SetActive(false);
            numeroPage++;
            if (numeroPage >= listePage.Length)
            {
                numeroPage = 0;
            }
            listePage[numeroPage].SetActive(true);
            timer = Time.time;
        }

        if (Input.GetAxis("Horizontal") < 0 && ((Time.time - timer) > 0.3f))
        {
            listePage[numeroPage].SetActive(false);
            numeroPage--;
            if (numeroPage < 0)
            {
                numeroPage = listePage.Length -1;
            }
            listePage[numeroPage].SetActive(true);
            timer = Time.time;
        }
	}
}
