using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {
	public float delayBetweenProjectiles = 4f;
	//public KeyCode moveUp;
	//public float		leftAndRightEdge = 50f; //might delete
	//public float		chanceToChangeDirections = 0.1f; //might delete
	public bool			playerHasBeenHit = false; //for collisions
	public bool 		attackMode = false; //not yet there
	public float 		bossSpeed = 5f;
	public Vector3 		pos;//his pos
	public int 			numProjectiles = 3;

	public float		closeDistanceThreshold = 6; //where he should stop near cam
	public float 		farDistanceThreshold = 12;	//where he should stop away from cam

	public GameObject	projectilePrefab;
	GameObject 	leftArm;

	public GUIText 		hitsGT;
	public int			enemyHP = 30;
	int			initEnemyHealth = 30;

	bool 		bossMoveAway = false;
	bool		bossMoveClose = true;
	int			counter = 0;
	


	// Use this for initialization
	void Start () {
		//hitsGO will be boss's health
		GameObject hitsGO = GameObject.Find ("HitCounter");
		hitsGT = hitsGO.GetComponent<GUIText> ();
		hitsGT.text = "Enemy Health: ";

		pos = this.transform.position;
		leftArm = GameObject.Find ("Left hand");
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (pos.x - Camera.main.transform.position.x);
	//	Debug.Log (closeDistanceThreshold);
	//	Debug.Log (!bossMoveAway);
	  if ((pos.x - Camera.main.transform.position.x) < closeDistanceThreshold && !bossMoveAway) {
						//he shal stop	
						//bossSpeed = 0f;
						if (enemyHP % 5 == 0 && enemyHP != initEnemyHealth) {
								bossMoveAway = true;
						}
						bossMoveClose = false;
						//same, but in the other direction, but the if clause needs to change to some distance from camera
		} 
		else if (bossMoveClose) {
						//basic movement coming towards you may need edits, may prevent backqards movement
						//bossSpeed = 10f;
						pos.x -= bossSpeed * Time.deltaTime;
						transform.position = pos;
	}
		//changing direction
		//if he gets within distance to camera 

		else if (pos.x - Camera.main.transform.position.x > farDistanceThreshold) {
						//bossSpeed = 0f;	;
						if (bossMoveAway) {
								throwProjectiles (numProjectiles);
						}
				bossMoveAway = false;
				} else if (bossMoveAway) {
						//bossSpeed = -10f;
						pos.x += bossSpeed * Time.deltaTime;
						transform.position = pos;
				} 
	}

	void throwProjectiles(int num) {
		counter = num;
		InvokeRepeating ("shootProjectile", 1f, delayBetweenProjectiles);
	}

	//On this collision, sword doesn't take damage
	void OnCollisionEnter(Collision coll) {
		GameObject collidedWith = coll.gameObject;
		if (collidedWith.tag == "Player") {
			Debug.Log ("Hit by player");
			playerHasBeenHit = true;
			attackMode = true;
			enemyHP -= 1;
		}
		//Debug.Log (enemyHP);

		hitsGT.text = "Enemy Health: " + enemyHP;
		if (enemyHP <= 0) {
			Debug.Log("beat the boss");
				Destroy (this.gameObject);
			Debug.Log("Boss is gone");
			Application.LoadLevel("__MYO_Scene_1");
		}
	}

	void shootProjectile(){
		if (--counter == 0) {
			CancelInvoke("shootProjectile");
			bossMoveClose = true;
		}
		GameObject projectile = Instantiate (projectilePrefab) as GameObject;
		Vector3 projectilePos = leftArm.transform.position;
		projectile.transform.position = projectilePos;
	}
}
