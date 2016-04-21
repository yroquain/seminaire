using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scriptHUD : MonoBehaviour {


    public GameObject HUDMort;
    private GameObject ElementAllie;
    public Sprite mageAirAllie;
    public Sprite mageEauAllie;
    public Sprite mageFeuAllie;
    public GameObject menuPause;

    private int numeroJoueur;
    private int numeroAllie;

    private float widthScreen;
    private GameObject barreManaMaxAllie;
    private GameObject barreHpMaxAllie;

    private GameController myGameController;
	// Use this for initialization
	void Start () {

            // Sort1Mask et Sort2mask géré dans le playerController.cs quand ils sont activés
            numeroJoueur = 0;
            numeroAllie = 1;
            if (GameObject.Find("Mage(Clone)") != null)
            {
                numeroJoueur = 1;
                numeroAllie = 0;
            }
            myGameController = GameObject.Find("networkManager").GetComponent<GameController>();
            widthScreen = Screen.width;
            GameObject.Find("Background").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.012f * widthScreen, 0);
            GameObject.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.23f, widthScreen*0.1f);

            GameObject.Find("Element").GetComponent<RectTransform>().position = new Vector3(.37f * widthScreen, 0.06f * widthScreen, 0);
            GameObject.Find("Element").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.1f, widthScreen * 0.1f);

            GameObject.Find("Sort1").GetComponent<RectTransform>().position = new Vector3(.469f * widthScreen, 0.055f * widthScreen, 0);
            GameObject.Find("Sort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

            GameObject.Find("BackgroundTextSort1").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.054f * widthScreen, 0);
            GameObject.Find("BackgroundTextSort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f * 1.5f, widthScreen * 0.015f);

            GameObject.Find("TextSort1").GetComponent<RectTransform>().position = new Vector3(.485f * widthScreen, 0.0595f * widthScreen, 0);
            GameObject.Find("TextSort1").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen  * 0.03334f, widthScreen * 0.02f);
        
            GameObject.Find("Sort2").GetComponent<RectTransform>().position = new Vector3(.531f * widthScreen, 0.055f * widthScreen, 0);
            GameObject.Find("Sort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.047f, widthScreen * 0.047f);

            GameObject.Find("BackgroundTextSort2").GetComponent<RectTransform>().position = new Vector3(.5471f * widthScreen, 0.054f * widthScreen, 0);
            GameObject.Find("BackgroundTextSort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.01667f * 1.5f, widthScreen * 0.015f);

            GameObject.Find("TextSort2").GetComponent<RectTransform>().position = new Vector3(.5471f * widthScreen, 0.0595f * widthScreen, 0);
            GameObject.Find("TextSort2").GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.03334f, widthScreen * 0.02f);

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

            barreManaMaxAllie = GameObject.Find("ManaMaxAllie");
            barreHpMaxAllie = GameObject.Find("HealthMaxAllie");

            Component[] Barre = this.GetComponentsInChildren<Image>();
            foreach (Image a in Barre)
            {
                if (a.name == "ElementAllie")
                {
                    this.ElementAllie = a.gameObject;
                }
            }
        
	}
	
	// Update is called once per frame
	void Update () {
        GameObject MageClone = GameObject.Find("Mage(Clone)");
        if (MageClone != null)
        {
            ElementAllie.SetActive(true);

            ElementAllie.GetComponent<RectTransform>().position=new Vector2(0.05f*widthScreen, .7f*Screen.height);

            if (MageClone.tag == "Mage_Feu")
            {
                ElementAllie.GetComponent<Image>().sprite = mageFeuAllie;
            }
            if (MageClone.tag == "Mage_Eau")
            {
                ElementAllie.GetComponent<Image>().sprite = mageEauAllie;
            }
            if (MageClone.tag == "Mage_Air")
            {
                ElementAllie.GetComponent<Image>().sprite = mageAirAllie;
            }
            float curHpAllie = myGameController.getHpActual(numeroAllie);
            float maxHpAllie = myGameController.getMaxHp(numeroAllie);
            float curManaAllie = myGameController.getManaActual(numeroAllie);
            float maxManaAllie = myGameController.getMaxMana(numeroAllie);

            if (maxHpAllie == 0)
            {
                maxHpAllie = 1;
            }
            if (maxManaAllie == 0)
            {
                maxManaAllie = 1;
            }

            GameObject.Find("HealthAllie").GetComponent<RectTransform>().sizeDelta = new Vector2(60 * curHpAllie / maxHpAllie, 10);
            GameObject.Find("HealthAllie").GetComponent<RectTransform>().position = new Vector3(barreHpMaxAllie.GetComponent<RectTransform>().position.x - 60 * (maxHpAllie - curHpAllie) / (2 * maxHpAllie), barreHpMaxAllie.GetComponent<RectTransform>().position.y, 0);

            GameObject.Find("ManaAllie").GetComponent<RectTransform>().sizeDelta = new Vector2(60 * curManaAllie / maxManaAllie, 10);
            GameObject.Find("ManaAllie").GetComponent<RectTransform>().position = new Vector3(barreManaMaxAllie.GetComponent<RectTransform>().position.x - 60 * (maxManaAllie - curManaAllie) / (2 * maxManaAllie), barreManaMaxAllie.GetComponent<RectTransform>().position.y, 0);
        }
        else
        {
            ElementAllie.SetActive(false);
        }

        
	}

    public void setPlayerDead(bool _playerDead){
        HUDMort.SetActive(_playerDead);
    }

    public void showMenuPause(){
        menuPause.SetActive(true);
    }
}
