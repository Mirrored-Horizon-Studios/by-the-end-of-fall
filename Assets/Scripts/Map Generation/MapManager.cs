using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject ObjectAtPosition(Vector3 position){
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

		for(int i = 0; i < allObjects.Length; i++){
			if(allObjects[i].transform.position == position)
				return allObjects[i];
		}

		return null;
	}
}
