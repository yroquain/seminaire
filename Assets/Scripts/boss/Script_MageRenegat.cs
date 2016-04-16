using UnityEngine;
using System.Collections;

public class Script_MageRenegat : MonoBehaviour {

    private bool isActivate;
    public GameObject squelette;
    private ArrayList allSquelette;
	// Use this for initialization
	void Start () {
        allSquelette = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
	



	}

    public void invocateSquelette()
    {
        Vector3[] posIni = { new Vector3(-2, .5f, 65), new Vector3(-4, .5f, 65), new Vector3(2, .5f, 65), new Vector3(4, .5f, 65) };
        for (int i = 0; i < 4; i++)
        {
            GameObject squeletteInvocate = (GameObject)Instantiate(squelette, posIni[i], new Quaternion(0, 180, 0, 0));
            squeletteInvocate.GetComponent<SkeletonController>().joueurAttack = i % 2;
            squeletteInvocate.GetComponent<SkeletonController>().IsActivate = true;
            allSquelette.Add(squeletteInvocate);
        }
       
    }

    public void setActive(bool _isActive)
    {
        this.isActivate = _isActive;
    }
}
