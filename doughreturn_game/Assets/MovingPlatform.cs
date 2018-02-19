using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	private Rigidbody rb;
	private Vector3 start;
	private int direction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		start = transform.position;
		direction = 1;
	}

	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x > start.x + 0.2)
			direction = -1;
		else if (this.gameObject.transform.position.x < start.x - 0.2)
			direction = 1;
		rb.AddForce (5 * direction, 0, 0);
	}
}