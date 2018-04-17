using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScreenManager : MonoBehaviour
{
	public Button startButton;
	public Button helpButton;
	public Button creditsButton;
	public Button quitButton;
	public Button backButton;

	GameObject[] titleObjects;
	GameObject[] helpObjects;
	GameObject[] creditsObjects;
	GameObject[] backObjects;

	private void Start()
	{
		titleObjects = GameObject.FindGameObjectsWithTag("titleObject");
		helpObjects = GameObject.FindGameObjectsWithTag("helpObject");
		creditsObjects = GameObject.FindGameObjectsWithTag("creditsObject");
		backObjects = GameObject.FindGameObjectsWithTag ("backObject");
		showTitle();
	} 

	public void showTitle() {
		foreach (GameObject g in titleObjects) {
			g.SetActive (true);
		}
		foreach (GameObject h in helpObjects) {
			h.SetActive (false);
		}
		foreach (GameObject i in creditsObjects) {
			i.SetActive (false);
		}
		foreach (GameObject k in backObjects) {
			k.SetActive (false);
		}
	}

	public void showHelp() {
		foreach (GameObject g in titleObjects) {
			g.SetActive (false);
		}
		foreach (GameObject h in helpObjects) {
			h.SetActive (true);
		}
		foreach (GameObject i in creditsObjects) {
			i.SetActive (false);
		}
		foreach (GameObject k in backObjects) {
			k.SetActive (true);
		}
	}

	public void showCredits() {
		foreach (GameObject g in titleObjects) {
			g.SetActive (false);
		}
		foreach (GameObject h in helpObjects) {
			h.SetActive (false);
		}
		foreach (GameObject i in creditsObjects) {
			i.SetActive (true);
		}
		foreach (GameObject k in backObjects) {
			k.SetActive (true);
		}
	}

	private void OnEnable()
	{
		startButton.onClick.AddListener(startPressed);
		helpButton.onClick.AddListener(showHelp);
		creditsButton.onClick.AddListener(showCredits);
		backButton.onClick.AddListener(showTitle);
		quitButton.onClick.AddListener(quitPressed);
	}

	public void startPressed()
	{
		Application.LoadLevel(1);
	}

	public void quitPressed()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
		
	public void backPressed() {
		showTitle();
	}
}
