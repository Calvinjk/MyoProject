﻿using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll){
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Hands") {
			Debug.Log ("Successful Deflection");
		} else if (collidedWith.tag == "Projectile") {
			Debug.Log ("Successful Deflection");
		}
		
		
	}

}
