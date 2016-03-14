using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{

    public bool[] isCasting;
    public int[] numberSpell;
    public bool[] isAttacking;
    public bool[] isUsingSpell;
    public float[] manaActual;
    public float[] hpActual;
    public string[] elements;

    

	// Use this for initialization
	void Start () {
	    isCasting = new bool[2];
        isCasting[0] = false;
        isCasting[1] = false;
        numberSpell = new int[2];
        numberSpell[0] = 0;
        numberSpell[1] = 0;
        isAttacking = new bool[2];
        isAttacking[0] = false;
        isAttacking[1] = false;
        isUsingSpell = new bool[2];
        isUsingSpell[0] = false;
        isUsingSpell[1] = false;
        manaActual = new float[2];
        manaActual[0] = 0f;
        manaActual[1] = 0f;
        hpActual = new float[2];
        hpActual[0] = 0f;
        hpActual[1] = 0f;
        elements = new string[2];
        elements[0] = "null";
        elements[1] = "null";
	}
	
	// Update is called once per frame
	void Update () {
        if (isCasting[0] && isUsingSpell[1] || isCasting[1] && isUsingSpell[0])
        {
            Debug.Log("coucou");
        }
	}

    public void setHpManaActual(int _numberPlayer, float _hpActual, float _manaActual)
    {
        this.hpActual[_numberPlayer] = _hpActual;
        this.manaActual[_numberPlayer] = _manaActual;
    }

    public float getHpActual(int _numberPlayer)
    {
        return this.hpActual[_numberPlayer];
    }
    public float getManaActual(int _numberPlayer)
    {
        return this.manaActual[_numberPlayer];
    }
    
  
    public void SetIsCasting(bool _isCasting, int _numberPlayer, int _numberSpell)
    {
        this.isCasting[_numberPlayer] = _isCasting;
        this.isUsingSpell[_numberPlayer] = !_isCasting;
        this.numberSpell[_numberPlayer] = _numberSpell;
    }
    public void ResetVarSpell(int _numberPlayer)
    {
        this.isCasting[_numberPlayer] = false;
        this.isUsingSpell[_numberPlayer] = false;
        this.numberSpell[_numberPlayer] = 0;
    }
    public void SetElement(int _numberPlayer, string _element)
    {
        this.elements[_numberPlayer] = _element;
    }
    public bool IsotherCasting(int numeroautrejoueur)
    {
        return isCasting[numeroautrejoueur];
    }
    public bool IsotherSpelling(int numeroautrejoueur)
    {
        return isUsingSpell[numeroautrejoueur];
    }

}
