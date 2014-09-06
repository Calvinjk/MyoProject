using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {
	public KeyCode moveUp;
	public float		leftAndRightEdge = 50f; //might delete
	public float		chanceToChangeDirections = 0.1f; //might delete
	public bool			playerHasBeenHit = false; //for collisions
	public bool 		attackMode = false; //not yet there
	public float 		speed = 10f;
	public Vector3 		pos;//his pos

	public float		closeDistanceThreshold = 2; //where he should stop near cam
	public float 		farDistanceThreshold = 12;	//where he should stop away from cam

	public GameObject	projectilePrefab;
	public GameObject 	arms;

	public GUIText 		hitsGT;
	public int			enemyHP = 30;

	// Use this for initialization
	void Start () {
		//hitsGO will be boss's health
		GameObject hitsGO = GameObject.Find ("HitCounter");
		hitsGT = hitsGO.GetComponent<GUIText> ();
		hitsGT.text = "Enemy Health: ";

		pos = this.transform.position;
		arms = GameObject.Find ("Arms");
	}
	
	// Update is called once per frame
	void Update () {
		//basic movement
		pos.x -= speed * Time.deltaTime;
		transform.position = pos;
		//changing direction
		//if he gets within distance to camera 
		if (pos.x - Camera.main.transform.position.x < closeDistanceThreshold) {

			//he shal stop	
			speed = 0f;	
			//same, but in the other direction, but the if clause needs to change to some distance from camera
		} else if (pos.x - Camera.main.transform.position.x > farDistanceThreshold) {

				speed = 0f;	
			//shootProjectile();
		}
					}

	void FixedUpdate(){
			/*	if (Random.value < chanceToChangeDirections) {
						speed *= -1; 
				}*/
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
		GameObject projectile = Instantiate (projectilePrefab) as GameObject;
		projectile.transform.position = arms.transform.position;

		//projectile.transform.position.x = transform.position.x + 10;
	}
}
