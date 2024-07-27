using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour {

	[SerializeField] int buttonIndexValue = 0;

	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		audioSource.volume = FindObjectOfType<GameSettings>().getFXVolume();
	}

	public void setButtonIndexValue(int indexValue){
		buttonIndexValue = indexValue;
	}

	public int getButtonIndexValue(){
		return buttonIndexValue;
	}

	public void activatePowerUp(){
		FindObjectOfType<GameSession>().activatePowerUp(buttonIndexValue);
		GetComponent<AudioSource>().Play();
	}
}
