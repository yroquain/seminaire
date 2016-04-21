using UnityEngine;
using System.Collections;

public class Script_MageRenegat : MonoBehaviour {

    private bool isActivate;
    public GameObject squelette;
    private ArrayList allSquelette;
    private Animator anim;

    public GameObject pillier1;
    public GameObject pillier2;
    public GameObject pillier3;


	// Use this for initialization
	void Start () {
        allSquelette = new ArrayList();
        anim = GetComponent<Animator>();
        isActivate = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActivate)
        {
            GameObject listeSquelette = GameObject.Find("skeleton_animated(Clone)");
            if (isActivate && listeSquelette == null)
            {
                invocateSquelette();
            }
        }
        if (pillier1 == null && pillier2 == null && pillier3 == null)
        {
            isDead();
        }
	}

    public void invocateSquelette()
    {
        anim.SetBool("invoqueSquelette", true);
        Vector3[] posIni = { new Vector3(-2, .5f, 65), new Vector3(-4, .5f, 65), new Vector3(2, .5f, 65), new Vector3(4, .5f, 65) };
        for (int i = 0; i < 4; i++)
        {
            GameObject squeletteInvocate = (GameObject)Instantiate(squelette, posIni[i], new Quaternion(0, 180, 0, 0));
            squeletteInvocate.GetComponent<SkeletonController>().joueurAttack = i % 2;
            squeletteInvocate.GetComponent<SkeletonController>().IsActivate = true;
            allSquelette.Add(squeletteInvocate);
        }
        anim.SetBool("invoqueSquelette", false);
    }

    private void isDead()
    {
        anim.SetBool("isDead", true);
        isActivate = false;
    }

    public void setActive(bool _isActive)
    {
        this.isActivate = _isActive;
    }

    
}
