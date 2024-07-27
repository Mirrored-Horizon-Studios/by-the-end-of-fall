using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
	[SerializeField] Transform target;
	[SerializeField] float offsetX = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target){
			transform.position = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
		}
	}
}
