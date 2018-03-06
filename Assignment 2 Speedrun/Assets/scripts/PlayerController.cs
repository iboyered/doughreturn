using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody pMovement;

	private float moveSpeed = 10.0f;
	private float jumpSpeed = 10.0f;
	private float moveX;
	private bool ground;

	// Use this for initialization
	void Start () {
		pMovement = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
