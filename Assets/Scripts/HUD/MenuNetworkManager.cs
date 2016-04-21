using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuNetworkManager : MonoBehaviour {

    private ArrayList menuList = new ArrayList();
    private int selectingOption = 0;
    private int numberMenu = 3;

    private Text creerMatch;
    private Text rejoindreMatch;
    private Text quitterMatchMaking;

    public GameObject panelNetwork;
    public NetworkManager manager;

    private float timer = 0.0f;
    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < numberMenu; i++)
        {
            menuList.Add(i);
        }
        creerMatch = GameObject.Find("txt_creerMatch").GetComponent<Text>();
        rejoindreMatch = GameObject.Find("txt_trouverUnMatch").GetComponent<Text>();
        quitterMatchMaking = GameObject.Find("txt_quitterMatchMaking").GetComponent<Text>();
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
                manager.matchMaker.CreateMatch(manager.matchName, manager.matchSize, true, "", manager.OnMatchCreate);
                this.gameObject.SetActive(false);
            }
            else if (selectingOption == 1)
            {
                manager.matchMaker.ListMatches(0, 20, "", manager.OnMatchList);
                this.gameObject.SetActive(false);
            }
            else
            {
                manager.StopMatchMaker();
                panelNetwork.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }

        }

        //add color to the selected text
        switch (selectingOption)
        {
            case 0:
                creerMatch.color = Color.red;
                rejoindreMatch.color = Color.black;
                quitterMatchMaking.color = Color.black;
                break;

            case 1:
                creerMatch.color = Color.black;
                rejoindreMatch.color = Color.red;
                quitterMatchMaking.color = Color.black;
                break;

            case 2:
                creerMatch.color = Color.black;
                rejoindreMatch.color = Color.black;
                quitterMatchMaking.color = Color.red;
                break;
        
            default:
                break;
        }
    }
}
