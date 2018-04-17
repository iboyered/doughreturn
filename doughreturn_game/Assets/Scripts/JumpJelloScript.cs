using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpJelloScript : MonoBehaviour {

	Transform trans;
	Vector3 full = Vector3.one * 2.5f;
	Vector3 squish1 = new Vector3(2.5f, 1f, 2.5f);
	//Vector3 squish2 = new Vector3(2.5f, 1.75f, 2.5f);
	bool touchingPlayer;
	Vector3 loc, startLoc;

	void Start() {
		trans = this.transform;
		trans.localScale = full;
		startLoc = trans.position;
	}

	public void Bounce (){
		
	}

	void FixedUpdate() {
		
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			touchingPlayer = true;
			StartCoroutine(Morph ());
		}
	}
	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			touchingPlayer = false;
		}
	}

	IEnumerator Morph() {
		trans.localScale = squish1;
		loc = startLoc;
		loc.y -= 0.5f;
		trans.position = loc;
		yield return new WaitForSeconds (0.5f);
		yield return new WaitUntil(() => !touchingPlayer);
		trans.localScale = full;
		trans.position = startLoc;
	}

}
