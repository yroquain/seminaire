using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectMenuPauseTuto : MonoBehaviour {

    private ArrayList menuList = new ArrayList();
    private int selectingOption = 0;
    private int numberMenu = 4;

    private Text resume;
    private Text grimoire;
    private Text mainMenu;
    private Text affichertuto;

    public GameObject Alamanach;
    public GameObject AlmanachFeu;
    public GameObject AlmanachEau;
    public GameObject AlmanachAir;
    public GameObject TutoPage1;
    public GameObject LeCanvasTuto;

    /* 0 : reprendre
     * 1 : almanach
     = 2 : afficher tuto
     * 3 : retour au menu principal
     */


    private float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numberMenu; i++)
        {
            menuList.Add(i);
        }
        resume = GameObject.Find("txt_Reprendre").GetComponent<Text>();
        grimoire = GameObject.Find("txt_Grimoire").GetComponent<Text>();
        mainMenu = GameObject.Find("txt_Menu_Principal").GetComponent<Text>();
        affichertuto = GameObject.Find("txt_Afficher_tuto").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))
        {
            this.gameObject.SetActive(false);
            GameObject Player = GameObject.Find("LOCAL Player");
            if (Player != null)
            {
                GameObject.Find("LOCAL Player").GetComponent<PlayerController>().enabled = true;
            }
            else
            {
                GameObject.Find("MageTutorial").GetComponent<PCTuto>().enabled = true;
            }
        }

        //on descend dans la liste
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
                this.gameObject.SetActive(false);
                GameObject Player = GameObject.Find("LOCAL Player");
                if (Player != null)
                {
                    GameObject.Find("LOCAL Player").GetComponent<PlayerController>().enabled = true;
                }
                else
                {
                    GameObject.Find("MageTutorial").GetComponent<PCTuto>().enabled = true;
                }
            }
            else if (selectingOption == 1)
            {

                Alamanach.SetActive(true);
                this.gameObject.SetActive(false);
            }
            else if(selectingOption == 2)
            {
                LeCanvasTuto.SetActive(true);
                TutoPage1.SetActive(true);
                GameObject.Find("MageTutorial").GetComponent<PCTuto>().enabled = true;
                GameObject.Find("MageTutorial").GetComponent<PCTuto>().IsTutoActif = true;
                GameObject.Find("MageTutorial").GetComponent<PCTuto>().i = 1;
                this.gameObject.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene(0);
            }

        }

        //add color to the selected text
        switch (selectingOption)
        {
            case 0:
                resume.color = Color.red;
                grimoire.color = Color.black;
                affichertuto.color = Color.black;
                mainMenu.color = Color.black;
                break;
            case 1:
                resume.color = Color.black;
                grimoire.color = Color.red;
                affichertuto.color = Color.black;
                mainMenu.color = Color.black;
                break;
            case 2:
                resume.color = Color.black;
                grimoire.color = Color.black;
                affichertuto.color = Color.red;
                mainMenu.color = Color.black;
                break;
            case 3:
                resume.color = Color.black;
                grimoire.color = Color.black;
                affichertuto.color = Color.black;
                mainMenu.color = Color.red;
                break;
            default:
                break;
        }
    }
}
