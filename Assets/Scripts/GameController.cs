using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{

    public bool[] isCasting;
    public int[] numberSpell;
    public bool[] isActivate;
    public bool[] isUsingSpell;
    public float[] manaActual;
    public float[] hpActual;
    public float[] manaMax;
    public float[] hpMax;
    public string[] elements;
    public bool[] isAttacking;
    public bool[] isReady;

    private bool GameHasStarted;

	// Use this for initialization
	void Start ()
    {
        GameHasStarted = false;
        isCasting = new bool[2];
        isCasting[0] = false;
        isCasting[1] = false;
        isAttacking = new bool[2];
        isAttacking[0] = false;
        isAttacking[1] = false;
        numberSpell = new int[2];
        numberSpell[0] = 0;
        numberSpell[1] = 0;
        isActivate = new bool[2];
        isActivate[0] = false;
        isActivate[1] = false;
        isUsingSpell = new bool[2];
        isUsingSpell[0] = false;
        isUsingSpell[1] = false;
        manaActual = new float[2];
        manaActual[0] = 0f;
        manaActual[1] = 0f;
        hpActual = new float[2];
        hpActual[0] = 0f;
        hpActual[1] = 0f;
        manaMax = new float[2];
        manaMax[0] = 0f;
        manaMax[1] = 0f;
        hpMax = new float[2];
        hpMax[0] = 0f;
        hpMax[1] = 0f;
        elements = new string[2];
        elements[0] = "Mage_Feu";
        elements[1] = "Mage_Feu";
        isReady = new bool[2];
        isReady[0] = false;
        isReady[1] = false;
    }
	
	// Update is called once per frame
	void Update () {
       if(isReady[0] && isReady[1] && !GameHasStarted)
        {
            GameHasStarted = true;
            GameObject.Find("LOCAL Player").GetComponent<PlayerController>().StartGame();
        }
	}

    public void setHpManaActual(int _numberPlayer, float _hpActual, float _manaActual, float _maxHp, float _maxMana)
    {
        this.hpActual[_numberPlayer] = _hpActual;
        this.manaActual[_numberPlayer] = _manaActual;
        this.hpMax[_numberPlayer] = _maxHp;
        this.manaMax[_numberPlayer] = _maxMana;
    }

    public float getHpActual(int _numberPlayer)
    {
        return this.hpActual[_numberPlayer];
    }
    public float getManaActual(int _numberPlayer)
    {
        return this.manaActual[_numberPlayer];
    }
    public float getMaxHp(int _numberPlayer)
    {
        return this.hpMax[_numberPlayer];
    }
    public float getMaxMana(int _numberPlayer)
    {
        return this.manaMax[_numberPlayer];
    }

   
    public bool getTwoTriggerActivate(){
        if (this.isActivate[0] && this.isActivate[1])
        {
            return true;
        }
        else
        {
            return false;
        }
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
    public void Ready(int numero)
    {
        isReady[numero] = !isReady[numero];
    }

    [ClientRpc]
    public void RpcSetIsActivate(int _numeroTrigger, bool _isActivate)
    {
        this.isActivate[_numeroTrigger] = _isActivate;
    }
    
}
