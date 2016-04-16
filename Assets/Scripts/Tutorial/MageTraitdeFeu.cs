using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MageTraitdeFeu : MonoBehaviour {
    
    public GameObject Mycamera;
    public Transform pos;
    public bool IsActivated;
    public float TimeBeforeActivated;
    private bool ActivatedOnce;
    private float TimeBeforeUnactivated;
    public Material mage;
    public GameObject Message;
    public GameObject[] Prefabs;
    private bool IsMessageDesactivated;
    // Use this for initialization
    void Start()
    {
        transform.Find("Mage").GetComponent<Renderer>().material = mage;
        ActivatedOnce = false;
        GetComponent<Animation>().Play("CombatModeA");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated && Time.time < TimeBeforeActivated + 3.90f)
        {
            IsMessageDesactivated = false;
            Message.SetActive(true);
            Message.GetComponent<Text>().text = "Le sort sera activé dans:\n" + ((int)(TimeBeforeActivated + 4 - Time.time)).ToString();
        }
        if (IsActivated && Time.time > TimeBeforeActivated + 4 && !ActivatedOnce)
        {
            Message.SetActive(false);
            GameObject player = this.gameObject;
            Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
                player.transform.position.y + 2,
                player.transform.position.z + player.transform.forward.z * 2);


            Instantiate(Prefabs[1], position, Quaternion.identity);

            Vector3 pos;
            float yRot = transform.rotation.eulerAngles.y;
            Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
            Quaternion rotation = Quaternion.identity;
            GameObject currentPrefabObject = GameObject.Instantiate(Prefabs[0]);
            FireBaseScript currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

            if (currentPrefabScript == null)
            {
                // temporary effect, like a fireball
                currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
                if (currentPrefabScript.IsProjectile)
                {
                    // set the start point near the player
                    rotation = Mycamera.transform.rotation;
                    //rotation = transform.rotation;
                    pos = new Vector3(transform.position.x + Mycamera.transform.forward.x * 2,
                            transform.position.y + 2,
                            transform.position.z + Mycamera.transform.forward.z * 2); ;
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
            ActivatedOnce = true;
            TimeBeforeUnactivated = Time.time;
            GetComponent<Animation>().Play("Spell_Cast_C");
        }
        if (Time.time > TimeBeforeUnactivated + 1 && ActivatedOnce)
        {
            GetComponent<Animation>().Play("CombatModeA");
            ActivatedOnce = false;
            IsActivated = false;
        }
        if (!IsActivated && !ActivatedOnce && !IsMessageDesactivated)
        {
            GetComponent<Animation>().Play("CombatModeA");
            IsMessageDesactivated = true;
            Message.SetActive(false);
        }

    }
    public void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.name == "MageTutorial")
        {
            if (collide.gameObject.GetComponent<PCTuto>().IsAskingForSpell && !IsActivated)
            {

                GetComponent<Animation>().Play("Combat_Mode_C");
                IsActivated = true;
                TimeBeforeActivated = Time.time;
            }
            if (collide.gameObject.GetComponent<PCTuto>().getIsCasting() && IsActivated)
            {
                collide.gameObject.GetComponent<SortSimpleTuto>().otherspell = 5;
                IsActivated = false;
            }
        }
    }
    public void OnTriggerStay(Collider collide)
    {
        if (collide.gameObject.name == "MageTutorial")
        {
            if (collide.gameObject.GetComponent<PCTuto>().IsAskingForSpell && !IsActivated)
            {

                GetComponent<Animation>().Play("Combat_Mode_C");
                IsActivated = true;
                TimeBeforeActivated = Time.time;
            }
            if (collide.gameObject.GetComponent<PCTuto>().getIsCasting() && IsActivated)
            {
                collide.gameObject.GetComponent<SortSimpleTuto>().otherspell = 5;
                IsActivated = false;
            }
        }
    }
}
