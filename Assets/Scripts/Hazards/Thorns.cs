using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour {
	[SerializeField] GameObject explosionVFX;
	[SerializeField] AudioClip destroySound;
	[Range(0,1)][SerializeField] float soundFXVolume = .5f;

	private GameSettings gameSettings;

	void Start(){
		gameSettings = FindObjectOfType<GameSettings>();		
	}
	void OnCollisionEnter2D(Collision2D other){
		Player player = other.gameObject.GetComponent<Player>();

		if(player){
			PowerUp activePowerup = FindObjectOfType<GameSession>().getActivePowerup();

			if(activePowerup != null && activePowerup.getName() == "Stone Squirrel"){
				DoVfx();
				DoSfx();
				Destroy(gameObject);
			}
			else{
				player.die();
			}
		}
	}

	void DoVfx(){
		var explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
		Destroy(explosion, 3);
	}

	void DoSfx(){
		AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, gameSettings.getFXVolume());
	}
}
