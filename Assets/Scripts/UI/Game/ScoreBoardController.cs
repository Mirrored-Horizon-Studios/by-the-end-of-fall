using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardController : MonoBehaviour {

	[SerializeField] GameObject scoreBoardWindow;
	[SerializeField] Button advertButton;
	[SerializeField] Image advertButtonImage;

	[SerializeField] TMP_Text currentScoreText, highScoreText;

	[SerializeField] AudioClip buttonClickSound;

	[SerializeField] GameObject AdmobManager;
	
	private GameSettings gameSettings;
	void Start () {
		gameSettings = FindObjectOfType<GameSettings>();
	}
	
	// Update is called once per frame
	void Update () {	
		if(AdmobManager.GetComponent<AdMob>().adIsLoaded()){
			advertButton.interactable = true;
			advertButtonImage.color = new Color(255, 255, 255, 255);
		}else{
			advertButton.interactable = false;
			advertButtonImage.color = new Color(159, 159, 159, 128);
		}
	}

	public void open(){
		if(scoreBoardWindow.activeInHierarchy == false){
			scoreBoardWindow.SetActive(true);
		}
	}

	public void setCurrentScore(string score){
		currentScoreText.text = score + " Acres";
	}

	public void setHighScoreText(string highScore){
		highScoreText.text = highScore + " Acres";
	}

	public void playAgainClicked(){
		StartCoroutine(loadGameDelay());
	}

	public void menuClicked(){
		StartCoroutine(loadMenuDelay());
	}

	IEnumerator loadGameDelay(){
		AudioSource.PlayClipAtPoint(buttonClickSound, Camera.main.transform.position, gameSettings.getFXVolume());
		yield return new WaitForSeconds(.3f);

		FindObjectOfType<SceneLoader>().LoadGameScene();		
	}

	 IEnumerator loadMenuDelay(){
		AudioSource.PlayClipAtPoint(buttonClickSound, Camera.main.transform.position, gameSettings.getFXVolume());
		yield return new WaitForSeconds(.3f);

		FindObjectOfType<SceneLoader>().LoadStartScene();
	}
}
