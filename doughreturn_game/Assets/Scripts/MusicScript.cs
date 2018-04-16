using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	public AudioClip musicClip;
	public AudioSource musicSource;

	void Start () {
		musicSource.clip = musicClip;
	}
	

	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			musicSource.Play ();
		}
	}
}
