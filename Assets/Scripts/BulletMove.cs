using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {


	public int speedBullet = 5;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
			if(transform.position.x < player.transform.position.x){
				GetComponent<Rigidbody>().velocity = new Vector3(speedBullet, 0, 0);
				
			}
			//ennemi à droite
			else if(transform.position.x > player.transform.position.x){
				GetComponent<Rigidbody>().velocity = new Vector3(-speedBullet, 0, 0);
			}

	}
	
	// Update is called once per frame
	void Update () {
	}



}
