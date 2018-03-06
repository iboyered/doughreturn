using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour {

	//private int speed = 4;
	//private int moveRight = 1;
	private GameObject target=null;
	private Vector3 offset;

	public float min=2f;
	public float max=3f;
	// Use this for initialization
	void Start () {

		min=transform.position.x;
		max=transform.position.x+12;

	}

	// Update is called once per frame
	void Update () {


		transform.position =new Vector3(Mathf.PingPong(Time.time*3,max-min)+min, transform.position.y, transform.position.z);

	}

	// Collision stuff to keep the player on the platform
	// Not working for now
	void OnTriggerStay2D(Collider2D col){
		target = col.gameObject;
		offset = target.transform.position - transform.position;
	}
	void OnTriggerExit2D(Collider2D col){
		target = null;
	}
	void LateUpdate(){
		if (target != null) {
			target.transform.position = transform.position+offset;
		}
	}
}