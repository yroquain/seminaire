using UnityEngine;
using System.Collections;

public class EnnemyAction : MonoBehaviour {
	
	public Transform bullet;
	private float timeToWait = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition();

	}
	
	public void UpdatePosition(){
		ThrowBullet();
		
	}

	void ThrowBullet(){
		Vector3 position = new Vector3(0,0,0);
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		//ennemi venant de la droite
		if(tag == "RightEnnemy"){
			position = new Vector3(transform.position.x-1.0f,transform.position.y,transform.position.z);
		}

		//ennemi venant de la gauche
		else if (tag == "LeftEnnemy"){
			position = new Vector3(transform.position.x+1.0f,transform.position.y,transform.position.z);
		}

		//si une balle a correctement été initialisée, on la fait bouger.
		if(position != new Vector3(0,0,0)){
			if((Time.time - timeToWait) > 2){
				Transform newBullet = (Transform)Instantiate(bullet,position,Quaternion.identity);

				//ennemi à gauche du joueur
				if(transform.position.x < player.transform.position.x){
					newBullet.transform.Translate(Vector2.right * Time.deltaTime);

				}
				//ennemi à droite
				else if(transform.position.x > player.transform.position.x){
					newBullet.transform.Translate(Vector2.left * Time.deltaTime);
				}
				timeToWait = Time.time;
			}
		}

	}
}