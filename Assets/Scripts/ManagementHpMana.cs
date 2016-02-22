using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ManagementHpMana : NetworkBehaviour
{

    /* Numero des sorts
     * 0 : Erreur
     * 1 : Mur d'éole
     * 2 : Bourrasque infernale
     * 3 : Choc Aquatique
     * 4 : Pluie Divine
     * 5 : Trait de feu
     * 6 : Immolation
     */
    private int[] costManaSpell= {0,15,30,30,50,50,15};
    
    //Mana
    public float curMana;
    private float maxMana=100f;
    private float recupmana;

    //HP
    private float curHP;
    private float maxHP = 100.0f;

    //HUD
    private float widthScreen;
    private Component[] barre;
    private GameObject ManaBarre;
    private GameObject ManaBarreRef;
    private GameObject HealthBarre;
    private GameObject HealthBarreRef;



	// Use this for initialization
	void Start () {
        widthScreen = Screen.width;
        recupmana = Time.time;
        curMana = maxMana;
        curHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {

        //Management HP
        curHP += Time.deltaTime * 5;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
        if (curHP <= 0)
        {
            GetComponent<PlayerController>().CmdDeadPlayer(this.gameObject);
        }

        //Management Mana
        if (Time.time >= recupmana + 1)
        {
            recupmana = Time.time;
            if (curMana < maxMana)
            {
                if (curMana + 5 < maxMana)
                {
                    curMana += 5;
                }
                else
                {
                    curMana = maxMana;
                }
            }
        }
        if (HealthBarre && HealthBarreRef && ManaBarre && ManaBarreRef)
        {
            HealthBarre.GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f * curHP / maxHP, widthScreen * 0.012f);
            HealthBarre.GetComponent<RectTransform>().position = new Vector3(HealthBarreRef.GetComponent<RectTransform>().position.x - widthScreen * 0.156f * (maxHP-curHP) / (2*maxHP), HealthBarreRef.GetComponent<RectTransform>().position.y, 0);

            ManaBarre.GetComponent<RectTransform>().sizeDelta = new Vector2(widthScreen * 0.156f * curMana / maxMana, widthScreen * 0.012f);
            ManaBarre.GetComponent<RectTransform>().position = new Vector3(ManaBarreRef.GetComponent<RectTransform>().position.x - widthScreen * 0.156f * (maxMana-curMana) / (2*maxMana), ManaBarreRef.GetComponent<RectTransform>().position.y, 0);
        }
        this.CmdSynchronizeMana();
	}


    [Command]
    private void CmdSynchronizeMana()
    {
        this.gameObject.GetComponent<NetworkedPlayerScript>().RpcSynchronizeMana(this.gameObject, this.curMana);
    }
    public void setFullMana()
    {
        this.curMana = maxMana;
    }
    public void setFullHp()
    {
        this.curHP = maxHP;
    }
    public void setManaBarre(GameObject manaBarre)
    {
        this.ManaBarre = manaBarre;
    }
    public void setManaBarreRef(GameObject manaBarreRef)
    {
        this.ManaBarreRef = manaBarreRef;
    }
    public void setHealthBarre(GameObject healthBarre)
    {
        this.HealthBarre = healthBarre;
    }
    public void setHealthBarreRef(GameObject healthBarreRef)
    {
        this.HealthBarreRef = healthBarreRef;
    }
    public float getMaxMana()
    {
        return this.maxMana;
    }
    public float getCurMana()
    {
        return this.curMana;
    }
    public void setCurMana(float _curMana)
    {
        this.curMana=_curMana;
    }
    public void removeMana(float manaRemove)
    {
        this.curMana -= manaRemove;
    }
    public float getCurHp()
    {
        return this.curHP;
    }
    public void removeHp(float hpRemove)
    {
        this.curHP -= hpRemove;
    }
    public float getMaxHp()
    {
        return this.maxHP;
    }
    public int getCostManaSpell(int numberSpell){
        return this.costManaSpell[numberSpell];
    }
    public void removeManaFromSpell(int numberSpell)
    {
        this.curMana -= this.costManaSpell[numberSpell];
    }
}
