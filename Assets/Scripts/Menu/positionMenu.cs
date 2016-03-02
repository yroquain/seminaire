using UnityEngine;
using System.Collections;

public class positionMenu : MonoBehaviour {

    private float widthScreen;
    private float heightScreen;

    void Start () {
        widthScreen = Screen.width;
        heightScreen = Screen.height;
        GameObject.Find("Nouvelle Partie").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.77f * heightScreen, 0); //0.77
        GameObject.Find("Tutoriel").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.67f * heightScreen, 0); //0.67
        GameObject.Find("Options").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.57f * heightScreen, 0); //0.57
        GameObject.Find("Quitter Jeu").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.47f * heightScreen, 0);  //0.47

    }
}
