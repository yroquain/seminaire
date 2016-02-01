﻿using UnityEngine;
using System.Collections;

public class Sorts_Feu : MonoBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject camera;
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private int currentPrefabIndex;
    private FireBaseScript currentPrefabScript;

    public GameObject trait;

    #region Initialisation
    void Start()
    {
        this.damage = 10.0f;
        this.manaCost = 10.0f;
        this.CD = 10.0f;
        this.range = 100.0f;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

    }


    public void CastSpell(int numberSpell)
    {
        if (numberSpell == 1) //trait de feu
        {
            /*GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
            Vector3 position = new Vector3(player.transform.position.x + camera.transform.forward.x * 2,
                player.transform.position.y + 2,
                player.transform.position.z + camera.transform.forward.z * 2);
            Instantiate(trait, position, Quaternion.identity);
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;*/
            BeginEffect(numberSpell - 1);
        }
        //Immolation
        else if (numberSpell == 2)
        {
            throw new System.Exception("not implemented yet");
        }


    }

    private void BeginEffect(int i)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
        currentPrefabIndex = i;
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = camera.transform.rotation;
                //rotation = transform.rotation;
                pos = new Vector3(player.transform.position.x + camera.transform.forward.x * 2,
                        player.transform.position.y + 2,
                        player.transform.position.z + camera.transform.forward.z * 2); ;
                //pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other friendly layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }
}