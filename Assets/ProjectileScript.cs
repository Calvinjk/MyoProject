using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	float projectileSpeed = 10f;
	public static float bottomX = -15f;
	public static float swordLength = 3f;
	public static float distanceX = 19.5f;
	public static float zShift = 1.5f;
	public static float yShift = -1.5f;
	public float yVelocity = 0;
	public float zVelocity = 0;

	// Use this for initialization
	void Start () {
		yVelocity = Random.value * swordLength * this.projectileSpeed / distanceX + yShift;
		zVelocity = Random.value * swordLength * this.projectileSpeed / distanceX + zShift;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += -projectileSpeed * Time.deltaTime;
		pos.y += yVelocity * Time.deltaTime;
		pos.z += zVelocity * Time.deltaTime;
		transform.position = pos;

		//add code for sword hitting the projectile here
		if (transform.position.x < bottomX) {
			Destroy(this.gameObject);
		}
	}


}
