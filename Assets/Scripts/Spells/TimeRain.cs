using UnityEngine;
using System.Collections;

public class TimeRain : MonoBehaviour {

    private int Degat;
    private float timebeforedeath;
	// Use this for initialization
	void Start () {
        timebeforedeath = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Time.time> timebeforedeath+7)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider Coll)
    {
        if(Coll.gameObject.tag=="ennemi" && tag != "FireRain")
        {
            //Slow les ennemis
        }
        if(Coll.gameObject.tag == "ennemi" && tag == "FireRain")
        {
            //Reduit HP
        }
        if(Coll.gameObject.name== "Trait_Feu(Clone)")
        {
            //Coll.gameObject.GetComponent<Velocity_Trait_Feu>().Degat = valeur_boostee;
        }
    }
    public void OnTriggerStay(Collider Coll)
    {

        if (Coll.gameObject.tag == "ennemi" && tag!="FireRain")
        {
            //Slow les ennemis
        }
        if (Coll.gameObject.tag == "ennemi" && tag == "FireRain")
        {
            //Reduit HP
        }
        if (Coll.gameObject.tag == "ennemi" && tag == "Typhon")
        {
            float x = Random.Range(0, 1);
            float y = Random.Range(0, 1);
            float z = Random.Range(0, 1);
            Coll.gameObject.transform.position = new Vector3(Coll.gameObject.transform.position.x+x, 
                Coll.gameObject.transform.position.y+y, 
                Coll.gameObject.transform.position.z+z);
        }
        if (Coll.gameObject.name == "Trait_Feu(Clone)")
        {
            //Coll.gameObject.GetComponent<Velocity_Trait_Feu>().Degat = valeur_boostee;
        }
    }
}
