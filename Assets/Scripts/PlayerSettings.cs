using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour {
[SerializeField] Text highScoreText;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("high_score")){
			highScoreText.text = PlayerPrefs.GetInt("high_score").ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
