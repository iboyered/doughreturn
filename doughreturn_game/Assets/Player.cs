using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Rigidbody rb;
	public bool isGrounded;
	static public Vector3 reset;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		isGrounded = false;
		reset = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {

		//x move
		if (Input.GetKey(KeyCode.LeftArrow)) rb.AddForce (-15, 0, 0);
		else if (Input.GetKey(KeyCode.RightArrow)) rb.AddForce (15, 0, 0);

		//y move
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (isGrounded) rb.AddForce (0, 400, 0);
		}
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "ground")
			isGrounded = true;
		if (other.gameObject.tag == "death")
			gameObject.transform.position = reset;
	}

	void OnCollisionExit(Collision other) {
		if (other.gameObject.tag == "ground")
			isGrounded = false;
	}
}
