using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuUI : MonoBehaviour {
	[SerializeField] SceneLoader sceneLoader;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		audioSource.volume = FindObjectOfType<GameSettings>().getFXVolume();
	}

	public void startButtonClicked(){
		GetComponent<AudioSource>().Play();

		sceneLoader.LoadGameScene();
	}

	public void quitButtonClicked(){
		GetComponent<AudioSource>().Play();

		sceneLoader.QuitGame();
	}

}
