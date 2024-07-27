using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour {
	[SerializeField] AudioClip collectSound;
	[Range(0,1)][SerializeField] float soundFXVolume = .5f;

	private GameSettings gameSettings;

	// Use this for initialization
	void Start () {
		gameSettings = FindObjectOfType<GameSettings>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position, gameSettings.getFXVolume());
	
			FindObjectOfType<GameSession>().AddAcorn();
			Destroy(gameObject);
			
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		GetComponent<Spinner>().stopRotation();
	}
}
