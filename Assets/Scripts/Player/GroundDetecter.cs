using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetecter : MonoBehaviour {
	// Configuration Params
	[SerializeField] Player player;

	//State Params
	int groundContactPoints = 0;
	
	void Start(){
		player.PlayerDied += OnPlayerDied;
	}
	// Update is called once per frame
	void Update () {
			if(groundContactPoints > 0){
				player.SetPlayerGrounded(true);
			}
			else{
				player.SetPlayerGrounded(false);
			}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "ground"){
			groundContactPoints++;		
		}	
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "ground"){
			groundContactPoints--;		
		}	
	}

	public void OnPlayerDied(){
		Destroy(gameObject);
	}
}
