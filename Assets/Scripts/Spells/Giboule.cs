using UnityEngine;
using System.Collections;

public class Giboule : MonoBehaviour {

    private float timebeforedeath;
    private float makeitrain;
    // Use this for initialization
    void Start()
    {
        makeitrain = 0;
        timebeforedeath = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timebeforedeath + 7)
        {
            Destroy(gameObject);
        }
        if(Time.time> makeitrain +1)
        {
            makeitrain =  Time.time;
            GameObject player = this.gameObject;
            Vector3 position = new Vector3(player.transform.position.x + player.transform.forward.x * 2,
            player.transform.position.y + 2,
            player.transform.position.z + player.transform.forward.z * 2);


        Instantiate(GameObject.Find("LOCAL Player").GetComponent<Sorts_simple>().Prefabs[1], position, Quaternion.identity);

        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        GameObject currentPrefabObject = GameObject.Instantiate(GameObject.Find("LOCAL Player").GetComponent<Sorts_simple>().Prefabs[0]);
        FireBaseScript currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = Quaternion.Euler(60, -60, 0);
                //rotation = transform.rotation;
                pos = new Vector3(transform.position.x + Random.Range(2F, 12.0F) ,
                        transform.position.y ,
                        transform.position.z + Random.Range(3.0F, -12.0F) ) ;
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
}
