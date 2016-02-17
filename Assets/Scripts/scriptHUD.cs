using UnityEngine;
using System.Collections;

public class scriptHUD : MonoBehaviour {

    

	// Use this for initialization
	void Start () {

            // Sort1Mask et Sort2mask géré dans le playerController.cs quand ils sont activés
        
            float widthScreen = Screen.width;
            GameObject.Find("Background").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.012f * widthScreen, 0);
            GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.23f, widthScreen*0.1f);

            GameObject.Find("Element").GetComponent<RectTransform>().position = new Vector3(.37f * widthScreen, 0.06f * widthScreen, 0);
            GameObject.Find("Element").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.1f, widthScreen * 0.1f);

            GameObject.Find("Sort1").GetComponent<RectTransform>().position = new Vector3(.469f * widthScreen, 0.055f * widthScreen, 0);
            GameObject.Find("Sort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

            GameObject.Find("Sort2").GetComponent<RectTransform>().position = new Vector3(.531f * widthScreen, 0.055f * widthScreen, 0);
            GameObject.Find("Sort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

            GameObject.Find("Health1").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.035f * widthScreen, 0);
            GameObject.Find("Health1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

            GameObject.Find("Health2").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.035f * widthScreen, 0);
            GameObject.Find("Health2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

            GameObject.Find("Mana1").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.02f * widthScreen, 0);
            GameObject.Find("Mana1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

            GameObject.Find("Mana2").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.02f * widthScreen, 0);
            GameObject.Find("Mana2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);
        
	}
	
	// Update is called once per frame
	void Update () {
   
	}
}
