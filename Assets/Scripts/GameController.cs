using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{

    public bool[] isCasting;
    public int[] numberSpell;
    public bool[] isAttacking;
    public bool[] isUsingSpell;
    

	// Use this for initialization
	void Start () {
	    isCasting = new bool[2];
        numberSpell = new int[2];
        isAttacking = new bool[2];
        isUsingSpell = new bool[2];
	}
	
	// Update is called once per frame
	void Update () {
        if (isCasting[0] && isUsingSpell[1] || isCasting[1] && isUsingSpell[0])
        {
            Debug.Log("Coucou");
        }
	}



    [ClientRpc]
    public void RpcSetIsCasting(bool _isCasting, int _numberPlayer, int _numberSpell)
    {
        if (_isCasting)
        {
            this.isCasting[_numberPlayer] = _isCasting;
        }
        else
        {
            this.isUsingSpell[_numberPlayer] = true;
        }
        this.numberSpell[_numberPlayer] = _numberSpell;
    }


}
