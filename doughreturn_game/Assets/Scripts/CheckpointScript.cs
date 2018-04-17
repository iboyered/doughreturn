using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

	public bool checkpointReached;
	public SpriteRenderer sr;
	public Sprite checkInactive, checkActive;

	void Start () {
		checkpointReached = false;
		sr = GetComponent<SpriteRenderer> ();
		//checkInactive = (Sprite)Resources.Load<Sprite>("checkRed");
		//checkActive = (Sprite)Resources.Load("checkBlue");
		//pos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (checkpointReached) {
			//gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("checkBlue") as Sprite;
			sr.sprite = checkActive;
		} else {
			//gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load ("checkRed") as Sprite;
			sr.sprite = checkInactive;
		}
	}

	void SetActive () {
		checkpointReached = true;
	}
}
