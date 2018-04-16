using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpo1 : MonoBehaviour {

	public float min;
	public float max;

	// Use this for initialization
	void Start () {

		min=transform.position.x;
		max=transform.position.x+6;

	}

	// Update is called once per frame
	void Update () {

		transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z);

	}
}