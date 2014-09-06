using UnityEngine;
using System.Collections;

public class ArmsScript : MonoBehaviour {

	public GameObject parent;
	public Vector3 pos;

	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("Enemy");
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		pos = parent.transform.position;
		pos.x -= 1;
		//pos.y += 1;
		transform.position = pos;
		//Debug.Log (pos + " , " + parent.transform.position);
	}
}
