using UnityEngine;
using System.Collections;

public class scriptAlmanach : MonoBehaviour {

    public GameObject menuPause;
    private int numeroPage;
    public GameObject[] listePage;
    private float timer = 0.0f;
    public GameObject[] TextSortComb;
    public GameObject[] ImgSortComb;


    // Use this for initialization
    void Start () {
        numeroPage = 0;
        listePage[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))&& this.gameObject.GetComponentsInChildren<Transform>().Length > 0)
        {
            menuPause.SetActive(true);
            this.gameObject.SetActive(false);
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
            ChangerPage(numeroPage);
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
            ChangerPage(numeroPage);
        }
	}
    private void ChangerPage(int numero)
    {
        if (numero == 3)
        {
            if (PlayerPrefs.GetFloat("MurIfrit") == 0)
            {
                TextSortComb[0].SetActive(false);
                ImgSortComb[0].SetActive(false);
            }
            else
            {
                TextSortComb[0].SetActive(true);
                ImgSortComb[0].SetActive(true);
            }
            if (PlayerPrefs.GetFloat("BarriereGivree") == 0)
            {
                TextSortComb[1].SetActive(false);
                ImgSortComb[1].SetActive(false);
            }
            else
            {
                TextSortComb[1].SetActive(true);
                ImgSortComb[1].SetActive(true);
            }
        }
        if (numero == 4)
        {
            if (PlayerPrefs.GetFloat("PluiedeFeu") == 0)
            {
                TextSortComb[2].SetActive(false);
                ImgSortComb[2].SetActive(false);
            }
            else
            {
                TextSortComb[2].SetActive(true);
                ImgSortComb[2].SetActive(true);
            }
            if (PlayerPrefs.GetFloat("Giboulee") == 0)
            {
                TextSortComb[3].SetActive(false);
                ImgSortComb[3].SetActive(false);
            }
            else
            {
                TextSortComb[3].SetActive(true);
                ImgSortComb[3].SetActive(true);
            }

        }
        if (numero == 5)
        {
            if (PlayerPrefs.GetFloat("Typhon") == 0)
            {
                TextSortComb[4].SetActive(false);
                ImgSortComb[4].SetActive(false);
            }
            else
            {
                TextSortComb[4].SetActive(true);
                ImgSortComb[4].SetActive(true);
            }
            if (PlayerPrefs.GetFloat("TornadeEnflammee") == 0)
            {
                TextSortComb[5].SetActive(false);
                ImgSortComb[5].SetActive(false);
            }
            else
            {
                TextSortComb[5].SetActive(true);
                ImgSortComb[5].SetActive(true);
            }
        }
        if (numero == 6)
        {
            if (PlayerPrefs.GetFloat("FlecheMortelle") == 0)
            {
                TextSortComb[6].SetActive(false);
                ImgSortComb[6].SetActive(false);
            }
            else
            {
                TextSortComb[6].SetActive(true);
                ImgSortComb[6].SetActive(true);
            }
            if (PlayerPrefs.GetFloat("JetObsidienne") == 0)
            {
                TextSortComb[7].SetActive(false);
                ImgSortComb[7].SetActive(false);
            }
            else
            {
                TextSortComb[7].SetActive(true);
                ImgSortComb[7].SetActive(true);
            }
        }
        if (numero == 7)
        {
            if (PlayerPrefs.GetFloat("Raz") == 0)
            {
                TextSortComb[8].SetActive(false);
                ImgSortComb[8].SetActive(false);
            }
            else
            {
                TextSortComb[8].SetActive(true);
                ImgSortComb[8].SetActive(true);
            }

        }
    }
}
