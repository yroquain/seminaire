using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Sorts_Feu : NetworkBehaviour
{



    public float damage;
    public float manaCost;
    public float CD;
    public float range; //définit la portée de l'attaque
    public GameObject cameraa;
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private int currentPrefabIndex;
    private FireBaseScript currentPrefabScript;

    private bool castTraitDeFeu;
    private float timeCast;
    private float timeCastMax = 2f;

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

        if (castTraitDeFeu)
        {
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == true)
            {
                timeCast += Time.deltaTime;
            }
            if (this.gameObject.GetComponent<PlayerController>().getIsCasting() == false || timeCast > timeCastMax)
            {
                if (castTraitDeFeu)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
                    Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
                        player.transform.position.y + 2,
                        player.transform.position.z + player.transform.forward.z * 2);
                    Instantiate(Prefabs[1], position, Quaternion.identity);
                    //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;*/
                    BeginEffect(0);
                    castTraitDeFeu = false;
                }


                timeCast = 0f;
                this.gameObject.GetComponent<PlayerController>().setIsCasting(false);
            }

        }
    }


    public void CastSpell(int numberSpell)
    {
        if (numberSpell == 1) //trait de feu
        {
            /*GameObject player = GameObject.FindGameObjectWithTag("Mage_Feu");
            Vector3 position = new Vector3(player.transform.position.x+player.transform.forward.x*2,
                player.transform.position.y + 2,
                player.transform.position.z+player.transform.forward.z * 2);
            Instantiate(Prefabs[1], position, Quaternion.identity);
            //obj.GetComponent<Rigidbody>().velocity= transform.GetComponent<Rigidbody>().velocity;
            BeginEffect(numberSpell - 1);*/
            castTraitDeFeu = true;
        }
        //Immolation
        else if (numberSpell == 2)
        {
            CmdImmolation();
            //GetComponent<PlayerController>().IsImmolating = !GetComponent<PlayerController>().IsImmolating;
        }


    }

    private void BeginEffect(int i)
    {
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
                rotation = cameraa.transform.rotation;
                //rotation = transform.rotation;
                pos = new Vector3(transform.position.x + cameraa.transform.forward.x*2,
                        transform.position.y + 2,
                        transform.position.z + cameraa.transform.forward.z* 2); ;
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

    [Command]
    private void CmdImmolation()
    {
        this.GetComponent<NetworkedPlayerScript>().RpcImmolation(this.gameObject);
    }
}