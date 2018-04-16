using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_horizontal : MonoBehaviour {

	public float min;
	public float max;
	public float dist = 6f;
	public bool flipped;
	Vector3 rotate;

	// Use this for initialization
	void Start () {

		min=transform.position.x;
		max=transform.position.x + dist;

	}

	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(Mathf.PingPong(Time.time*2,dist)+min, transform.position.y, transform.position.z);

		if (!flipped && transform.position.x > (max - 0.05f)) {
			flipped = true;
			transform.Rotate (0, 180, 0);
		} else if (flipped && transform.position.x < (min + 0.05f)) {
			flipped = false;
			transform.Rotate (0, 180, 0);
		}

	}
}