using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class selectFinJeu : MonoBehaviour {
    private ArrayList menuList = new ArrayList();
    private int selectingOption = 0;
    private int numberMenu = 2;

    private Text grimoire;
    private Text mainMenu;

    public GameObject Alamanach;
    public GameObject AlmanachFeu;
    public GameObject AlmanachEau;
    public GameObject AlmanachAir;

    private float timer = 0.0f;
	// Use this for initialization
	void Start () {

        for (int i = 0; i < numberMenu; i++)
        {
            menuList.Add(i);
        }
        grimoire = GameObject.Find("txt_Grimoire").GetComponent<Text>();
        mainMenu = GameObject.Find("txt_Menu_Principal").GetComponent<Text>();
	}
	
	// Update is called once per frame
    void Update()
    {
     

        if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("SelectMenu") < 0) && ((Time.time - timer) > 0.2f))
        {
            selectingOption = (selectingOption + 1) % numberMenu; //le modulo sert à retourner à 0 si on est déjà en bas.
            timer = Time.time;
        }

        //on monte dans la liste
        else if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("SelectMenu") > 0) && ((Time.time - timer) > 0.2f))
        {
            selectingOption = ((selectingOption - 1) + numberMenu) % numberMenu; //le "+numberMenu" permet de gérer les nombres négatifs
            timer = Time.time;
        }

        //Jump is the "A" button on gamepad
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
        {
            if (selectingOption == 0)
            {
                //reprendre 
                Alamanach.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else
            {

                Network.Disconnect();
                GameObject.Find("networkManager").GetComponent<NetworkManager>().StopHost();
                MasterServer.UnregisterHost();
                SceneManager.LoadScene(0);
            }

        }

        //add color to the selected text
        switch (selectingOption)
        {
            case 0:
                grimoire.color = Color.red;
                mainMenu.color = Color.black;
                break;
            case 1:
                grimoire.color = Color.black;
                mainMenu.color = Color.red;
                break;
           
            default:
                break;
        }
    }
}
