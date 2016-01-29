using UnityEngine;
using System.Collections;

public class PluieDivine : MonoBehaviour {

    //à enlever une fois les paramètres définitifs définis.
    #region variables
    public float damage;
    public float manaCost;
    public float CD;
    public string element;
    public float range; //définit la portée de l'attaque
    public int numberSpell;
    #endregion 

    //à mettre quand les paramètres définitifs seront définis.
    /* 
    //#region getter/setter
    public float damage {get; set;}
    public float manaCost { get; set;}
    public float CD { get; set;}
    public string element {get;set;}
    public float range {get;set;}
    public int numberSpell {get;set;}
    #endregion
    */

    #region Initialisation
    void Start(){
        this.damage = 10.0f;
        this.manaCost = 10.0f;
        this.CD = 10.0f;      
        this.element = "Water";
        this.range = 10.0f;
        this.numberSpell = 2;
    }

    #endregion

    // Update is called once per frame
    void Update () {

    }
}
