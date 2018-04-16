using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

	public AudioClip soundClip;
	public AudioSource soundSource;

	void Start () {
		soundSource.clip = soundClip;
	}

	void PlaySound () {
		soundSource.Play ();
	}
}
