using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	[SerializeField] new string name;
	[SerializeField] string description;
	[SerializeField] int cost;
	[SerializeField] float duration;

	[SerializeField] Sprite displayImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print("is happening");
	}

	public string getName(){
		return name;
	}

	public float getDuration(){
		return duration;
	}

	public int getCost(){
		return cost;
	}

	public Sprite getDisplayImage(){
		return displayImage;
	}
}
