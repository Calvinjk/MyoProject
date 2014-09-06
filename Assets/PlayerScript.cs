using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//On this collision, sword(player)takes damage
	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		Debug.Log ("collision happened");
		if (collidedWith.tag == "Hands") {
			Debug.Log ("Hit player with hand");
		} else if (collidedWith.tag == "Projectile") {
			Debug.Log ("Hit player with projectile");
		}
		
		
	}
}
