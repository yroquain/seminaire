using UnityEngine;
using System.Collections;

public class CanvaTuto : MonoBehaviour {

    
    

    private float widthScreen;
    
    // Use this for initialization
    void Start()
    {
        
        widthScreen = Screen.width;
        GameObject.Find("Background").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.012f * widthScreen, 0);
        GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.23f, widthScreen * 0.1f);

        GameObject.Find("Element").GetComponent<RectTransform>().position = new Vector3(.37f * widthScreen, 0.06f * widthScreen, 0);
        GameObject.Find("Element").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.1f, widthScreen * 0.1f);

        GameObject.Find("Sort1").GetComponent<RectTransform>().position = new Vector3(.469f * widthScreen, 0.055f * widthScreen, 0);
        GameObject.Find("Sort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

        GameObject.Find("BackgroundTextSort1").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.054f * widthScreen, 0);
        GameObject.Find("BackgroundTextSort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f, widthScreen * 0.01f);

        GameObject.Find("TextSort1").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.0595f * widthScreen, 0);
        GameObject.Find("TextSort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f, widthScreen * 0.01f);

        GameObject.Find("Sort2").GetComponent<RectTransform>().position = new Vector3(.531f * widthScreen, 0.055f * widthScreen, 0);
        GameObject.Find("Sort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

        GameObject.Find("BackgroundTextSort2").GetComponent<RectTransform>().position = new Vector3(.5471f * widthScreen, 0.054f * widthScreen, 0);
        GameObject.Find("BackgroundTextSort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f, widthScreen * 0.01f);

        GameObject.Find("TextSort2").GetComponent<RectTransform>().position = new Vector3(.5471f * widthScreen, 0.0595f * widthScreen, 0);
        GameObject.Find("TextSort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f, widthScreen * 0.01f);

        GameObject.Find("Health1").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.035f * widthScreen, 0);
        GameObject.Find("Health1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

        GameObject.Find("Health2").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.035f * widthScreen, 0);
        GameObject.Find("Health2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

        GameObject.Find("TextHealth").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.04f * widthScreen, 0);
        GameObject.Find("TextHealth").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.04f);

        GameObject.Find("Mana1").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.02f * widthScreen, 0);
        GameObject.Find("Mana1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

        GameObject.Find("Mana2").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.02f * widthScreen, 0);
        GameObject.Find("Mana2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.012f);

        GameObject.Find("TextMana").GetComponent<RectTransform>().position = new Vector3(0.5f * widthScreen, 0.025f * widthScreen, 0);
        GameObject.Find("TextMana").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f, widthScreen * 0.04f);
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
