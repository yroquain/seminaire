using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MageChocAqua : MonoBehaviour {
    
    public GameObject trait;
    public Transform pos;
    public bool IsActivated;
    public float TimeBeforeActivated;
    private bool ActivatedOnce;
    private float TimeBeforeUnactivated;
    public Material mage;
    public GameObject Message;
    private bool IsMessageDesactivated;
    // Use this for initialization
    void Start () {
        transform.Find("Mage").GetComponent<Renderer>().material = mage;
        ActivatedOnce = false;
        GetComponent<Animation>().Play("CombatModeA");
    }
	
	// Update is called once per frame
	void Update () {
        if(IsActivated && Time.time < TimeBeforeActivated + 3.90f)
        {
            IsMessageDesactivated = false;
            Message.SetActive(true);
            Message.GetComponent<Text>().text = "Le sort sera activé dans:\n" + ((int)(TimeBeforeActivated + 4 - Time.time)).ToString();
        }
        if(IsActivated && Time.time> TimeBeforeActivated+4 && !ActivatedOnce)
        {
            Message.SetActive(false);
            Vector3 position = new Vector3(pos.position.x,
                    pos.position.y,
                    pos.position.z);
            Instantiate(trait, position, Quaternion.identity);
            ActivatedOnce = true;
            TimeBeforeUnactivated = Time.time;
            GetComponent<Animation>().Play("Spell_Cast_C");
        }
        if(Time.time> TimeBeforeUnactivated+1 && ActivatedOnce)
        {
            GetComponent<Animation>().Play("CombatModeA");
            ActivatedOnce = false;
            IsActivated = false;
        }
        if(!IsActivated && !ActivatedOnce && !IsMessageDesactivated)
        {
            GetComponent<Animation>().Play("CombatModeA");
            IsMessageDesactivated = true;
            Message.SetActive(false);
        }

    }
    public void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "MageTutorial")
        {
            if (collide.gameObject.GetComponent<PCTuto>().IsAskingForSpell && !IsActivated)
            {

                GetComponent<Animation>().Play("Combat_Mode_C");
                IsActivated = true;
                TimeBeforeActivated = Time.time;
            }
            if (collide.gameObject.GetComponent<PCTuto>().getIsCasting() && IsActivated)
            {
                collide.gameObject.GetComponent<SortSimpleTuto>().otherspell = 3;
                IsActivated = false;
            }
        }
    }
    public void OnTriggerStay(Collider collide)
    {
        if (collide.gameObject.name == "MageTutorial")
        {
            if (collide.gameObject.GetComponent<PCTuto>().IsAskingForSpell && !IsActivated)
            {

                GetComponent<Animation>().Play("Combat_Mode_C");
                IsActivated = true;
                TimeBeforeActivated = Time.time;
            }
            if(collide.gameObject.GetComponent<PCTuto>().getIsCasting() && IsActivated)
            {
                collide.gameObject.GetComponent<SortSimpleTuto>().otherspell = 3;
                IsActivated = false;
            }
        }
    }
}
