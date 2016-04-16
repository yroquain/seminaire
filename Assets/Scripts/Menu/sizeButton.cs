using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sizeButton : MonoBehaviour
{
    private int littleFontSize = 20;
    private int bigFontSize = 30;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.height > 500)
        {
            GameObject.Find("txt_Nouvelle_Partie").GetComponent<Text>().fontSize = bigFontSize;
            GameObject.Find("txt_Tutoriel").GetComponent<Text>().fontSize = bigFontSize;
            GameObject.Find("txt_Options").GetComponent<Text>().fontSize = bigFontSize;
            GameObject.Find("txt_Quitter_Jeu").GetComponent<Text>().fontSize = bigFontSize;
        }
        else
        {
            GameObject.Find("txt_Nouvelle_Partie").GetComponent<Text>().fontSize = littleFontSize;
            GameObject.Find("txt_Tutoriel").GetComponent<Text>().fontSize = littleFontSize;
            GameObject.Find("txt_Options").GetComponent<Text>().fontSize = littleFontSize;
            GameObject.Find("txt_Quitter_Jeu").GetComponent<Text>().fontSize = littleFontSize;
        }
    }
}
