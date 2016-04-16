using UnityEngine;
using System.Collections;

public class SphereCollide : MonoBehaviour {

    public GameObject MyPlayer;
    public bool IsCollided;
	// Use this for initialization
	void Start () {
        IsCollided = false;
    }
	// Update is called once per frame
	void Update () {

    }
    public void OnTriggerEnter(Collider collide)
    {
        if (!IsCollided)
        {
            if (collide.gameObject.name == "Mage(Clone)")
            {
                Component[] test = collide.gameObject.GetComponentsInChildren<Component>();
                foreach (Component a in test)
                {
                    if (a.gameObject.name == "Eternal Flame")
                    {
                        IsCollided = true;
                        MyPlayer.GetComponent<Sorts_simple>().ImmolatingSpell = true;
                    }
                }
            }
        }
    }
    public void OnTriggerStay(Collider collide)
    {
        if (!IsCollided)
        {
            if (collide.gameObject.name == "Mage(Clone)")
            {
                Component[] test = collide.gameObject.GetComponentsInChildren<Component>();
                foreach (Component a in test)
                {
                    if (a.gameObject.name == "Eternal Flame")
                    {
                        IsCollided = true;
                        MyPlayer.GetComponent<Sorts_simple>().ImmolatingSpell = true;
                    }
                }
            }
        }
    }
}
