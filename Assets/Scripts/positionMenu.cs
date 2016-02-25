using UnityEngine;
using System.Collections;

public class positionMenu : MonoBehaviour {

    private float widthScreen;
    public float x;
    public float y;
    // Use this for initialization
    void Update () {
        widthScreen = Screen.width;
        Debug.Log(widthScreen);
        GameObject.Find("Nouvelle Partie").GetComponent<RectTransform>().position = new Vector3(0.48f * widthScreen, 0.37f * widthScreen, 0);
        GameObject.Find("Tutoriel").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.31f * widthScreen, 0);
        GameObject.Find("Options").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.25f * widthScreen, 0);
        GameObject.Find("Quitter Jeu").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.19f * widthScreen, 0);

    }
}
