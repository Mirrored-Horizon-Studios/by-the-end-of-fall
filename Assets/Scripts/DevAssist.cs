using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevAssist : MonoBehaviour {

	/* Quick access tools for development purpouses. Remove from all scenes uppon release. */
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Escape)){
			RestartScene();
		}
	}

	void RestartScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
