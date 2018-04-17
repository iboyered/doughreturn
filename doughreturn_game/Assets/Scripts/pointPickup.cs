using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointPickup : MonoBehaviour {

	public int amount;

	void FixedUpdate() {
		gameObject.transform.Rotate (0, 0, 4);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
			scoreAndTimeManager.ChangeScore (amount);
		}
	}
}