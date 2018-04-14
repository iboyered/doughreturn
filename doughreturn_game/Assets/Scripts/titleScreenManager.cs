using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScreenManager : MonoBehaviour
{
	public Button levelSelectButton;
	public Button optionButton;
	public Button creditsButton;
	public Button quitButton;

	GameObject[] titleObjects;
	GameObject[] optionObjects;
	GameObject[] ballObjects;

	private void Start()
	{
		titleObjects = GameObject.FindGameObjectsWithTag("titleScreenItem");
		ballObjects = GameObject.FindGameObjectsWithTag("ballSelect");
		hideLevels();
	} 

	public void hideLevels() {
		foreach(GameObject h in titleObjects) {
			h.SetActive(true);
		}
		foreach (GameObject i in ballObjects)
		{
			i.SetActive(false);
		}
	}

	public void showLevels() {
		foreach (GameObject h in titleObjects)
		{
			h.SetActive(false);
		}
		foreach (GameObject i in ballObjects)
		{
			i.SetActive(false);
		}
	}

	private void OnEnable()
	{
		levelSelectButton.onClick.AddListener(levelSelectPressed);
		quitButton.onClick.AddListener(quitPressed);
	}

	public void levelSelectPressed()
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

	/*
	public void levelSelectPressed() {
		showLevels();
	}
	*/

	public void backPressed() {
		hideLevels();
	}
}
