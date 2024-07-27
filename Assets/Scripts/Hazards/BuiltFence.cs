using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuiltFence : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		Player player = other.gameObject.GetComponent<Player>();

		if(player){
			FindObjectOfType<SceneLoader>().LoadStartScene();
		}
	}
}
