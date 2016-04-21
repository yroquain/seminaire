using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuNetwork : MonoBehaviour {

    private ArrayList menuList = new ArrayList();
    private int selectingOption = 0;
    private int numberMenu = 5;

    private Text lancerLocal;
    private Text clientLAN;
    private Text serveurLAN;
    private Text lancerMatchMaking;
    private Text retourMenu;


    public GameObject panelMatchmaking;
    public NetworkManager manager;
    private float timer = 0.0f;
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < numberMenu; i++)
        {
            menuList.Add(i);
        }
        lancerLocal = GameObject.Find("txt_lancerLocal").GetComponent<Text>();
        clientLAN = GameObject.Find("txt_clientLAN").GetComponent<Text>();
        serveurLAN = GameObject.Find("txt_serveurLAN").GetComponent<Text>();
        lancerMatchMaking = GameObject.Find("txt_lancerMatchMaking").GetComponent<Text>();
        retourMenu = GameObject.Find("txt_retourMenu").GetComponent<Text>();
        
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
                manager.StartHost();
                this.gameObject.SetActive(false);
            }
            else if (selectingOption == 1)
            {
                manager.StartClient();
                this.gameObject.SetActive(false);
            }
            else if (selectingOption == 2)
            {
                manager.StartServer();
            }
            else if (selectingOption == 3)
            {
                manager.StartMatchMaker();
                panelMatchmaking.gameObject.SetActive(true);
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
                lancerLocal.color = Color.red;
                clientLAN.color = Color.black;
                serveurLAN.color = Color.black;
                lancerMatchMaking.color = Color.black;
                retourMenu.color = Color.black;
                break;

            case 1:
                lancerLocal.color = Color.black;
                clientLAN.color = Color.red;
                serveurLAN.color = Color.black;
                lancerMatchMaking.color = Color.black;
                retourMenu.color = Color.black;
                break;

            case 2:
                lancerLocal.color = Color.black;
                clientLAN.color = Color.black;
                serveurLAN.color = Color.red;
                lancerMatchMaking.color = Color.black;
                retourMenu.color = Color.black;
                break;

            case 3:
                lancerLocal.color = Color.black;
                clientLAN.color = Color.black;
                serveurLAN.color = Color.black;
                lancerMatchMaking.color = Color.red;
                retourMenu.color = Color.black;
                break;

            case 4:
                lancerLocal.color = Color.black;
                clientLAN.color = Color.black;
                serveurLAN.color = Color.black;
                lancerMatchMaking.color = Color.black;
                retourMenu.color = Color.red;
                break;

            default:
                break;
        }
    }
}
