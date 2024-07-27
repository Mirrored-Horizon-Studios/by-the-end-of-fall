using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Probe : MonoBehaviour {

	MapManager mm;

	// Use this for initialization
	void Start () {
		mm = FindObjectOfType<MapManager>();
		CeckRight();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void CeckRight(){
		GameObject test = mm.ObjectAtPosition(transform.position + new Vector3(1,0,0));
		if(test){
			print("found somethin " + test.name);
		}
	}
}
