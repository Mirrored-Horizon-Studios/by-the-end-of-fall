using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShredder : MonoBehaviour {

	/*
		Destroys any objects uppon collision. Used to clean up generated game objects that are no longer
		within the players view.
	 */


	void OnTriggerEnter2D(Collider2D other){
		Destroy(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){
		Destroy(other.gameObject);
	}
}
