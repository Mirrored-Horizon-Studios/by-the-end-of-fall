using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	[SerializeField] float scrollSpeed = 0.5f;
	 Material backgroundMaterial;

	 bool scrolling = false;

	Vector2 offset;
	// Use this for initialization
	void Start (){
		backgroundMaterial = GetComponent<Renderer>().material;
		offset = new Vector2(scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(scrolling){
			backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
		}
	}

	public void StartScrolling(){
		scrolling = true;
	}

	public void StopScrolling(){
		scrolling = false;
	}
}
