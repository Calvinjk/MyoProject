using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {
	public float delayBetweenProjectiles = 4f;
	public KeyCode moveUp;
	public float		leftAndRightEdge = 50f; //might delete
	public float		chanceToChangeDirections = 0.1f; //might delete
	public bool			playerHasBeenHit = false; //for collisions
	public bool 		attackMode = false; //not yet there
	public float 		bossSpeed = 10f;
	public Vector3 		pos;//his pos

	public float		closeDistanceThreshold = 2; //where he should stop near cam
	public float 		farDistanceThreshold = 12;	//where he should stop away from cam

	public GameObject	projectilePrefab;
	public GameObject 	leftArm;

	public GUIText 		hitsGT;
	public int			enemyHP = 30;

	public bool 		bossMoveAway = true;
	public bool			bossMovesClose = false;
	int					counter = 0;



	// Use this for initialization
	void Start () {
		//hitsGO will be boss's health
		GameObject hitsGO = GameObject.Find ("HitCounter");
		hitsGT = hitsGO.GetComponent<GUIText> ();
		hitsGT.text = "Enemy Health: ";

		pos = this.transform.position;
		leftArm = GameObject.Find ("leftHand");
	}
	
	// Update is called once per frame
	void Update () {
		//basic movement coming towards you may need edits, may prevent backqards movement
		pos.x += bossSpeed * Time.deltaTime;
		transform.position = pos;
		//changing direction
		//if he gets within distance to camera 
		if (pos.x - Camera.main.transform.position.x < closeDistanceThreshold) {

			//he shal stop	
			bossSpeed = 0f;	
			//same, but in the other direction, but the if clause needs to change to some distance from camera
		} else if (pos.x - Camera.main.transform.position.x > farDistanceThreshold) {
			Debug.Log("stopped called");
			bossSpeed = 0f;	
			if (bossMoveAway) {
				Debug.Log("throwProjectiles called");
				throwProjectiles(3);
			}
			bossMoveAway = false;
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
		}
		GameObject projectile = Instantiate (projectilePrefab) as GameObject;
		projectile.transform.position = leftArm.transform.position;
	}
}
