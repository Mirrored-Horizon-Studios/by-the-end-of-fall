using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

[SerializeField] float minDegrees, maxDegrees;

private bool shouldRotate = true;
float rotation;
	// Use this for initialization
	void Start () {
		rotation = Random.Range(minDegrees, maxDegrees);
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldRotate){
			transform.Rotate(0, 0, rotation * Time.deltaTime);
		}
	}

	public void stopRotation(){
		shouldRotate = false;
	}
}
