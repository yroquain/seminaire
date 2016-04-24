using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class selectMenu : MonoBehaviour
{

    private ArrayList menuList = new ArrayList();
    private int selectingOption = 0;
    private int numberMenu = 4;

    private Text newGame;
    private Text tutorial;
    private Text options;
    private Text quitGame;

    private float timer = 0.0f;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numberMenu; i++)
        {
            menuList.Add(i);
        }
        newGame = GameObject.Find("txt_Nouvelle_Partie").GetComponent<Text>();
        tutorial = GameObject.Find("txt_Tutoriel").GetComponent<Text>();
        options = GameObject.Find("txt_Options").GetComponent<Text>();
        quitGame = GameObject.Find("txt_Quitter_Jeu").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //on descend dans la liste
        if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("SelectMenu") < 0) && ((Time.time - timer) > 0.3f))
        {

            selectingOption = (selectingOption + 1) % numberMenu; //le modulo sert à retourner à 0 si on est déjà en bas.
            timer = Time.time;
        }

        //on monte dans la liste
        else if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("SelectMenu") > 0) && ((Time.time - timer) > 0.3f))
        {
            selectingOption = ((selectingOption - 1) + numberMenu) % numberMenu; //le "+numberMenu" permet de gérer les nombres négatifs
            timer = Time.time;
        }

        //Jump is the "A" button on gamepad
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
        {
            if (selectingOption < (numberMenu - 1))
            {
                SceneManager.LoadScene(selectingOption + 1);
            }
            else
            {
                Application.Quit();
            }

        }

        //add color to the selected text
        switch (selectingOption)
        {
            case 0:
                newGame.color = Color.red;
                tutorial.color = Color.black;
                options.color = Color.black;
                quitGame.color = Color.black;
                break;
            case 1:
                newGame.color = Color.black;
                tutorial.color = Color.red;
                options.color = Color.black;
                quitGame.color = Color.black;
                break;
            case 2:
                newGame.color = Color.black;
                tutorial.color = Color.black;
                options.color = Color.red;
                quitGame.color = Color.black;
                break;
            case 3:
                newGame.color = Color.black;
                tutorial.color = Color.black;
                options.color = Color.black;
                quitGame.color = Color.red;
                break;
            default:
                break;
        }
    }
}
