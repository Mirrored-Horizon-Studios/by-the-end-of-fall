using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GUIController : MonoBehaviour {

	[SerializeField] TMP_Text startText;
	[SerializeField] Text scoreText, acornCountText;
	[SerializeField] Image acornImage;

	[SerializeField] GameObject optionsButton;
	[SerializeField] GameObject userInput;

	[SerializeField] PowerUpTimer powerUpTimer;

	[SerializeField] PowerUpButton[] powerUpButtons;

	public void updateScoreText(string score){
		scoreText.text = score;
	}

	public void updateAcornText(string acornCount){
		acornCountText.text = acornCount;
	}

	public void activateTextViews(){
		acornImage.gameObject.SetActive(true);
		
		scoreText.gameObject.SetActive(true);
		acornCountText.gameObject.SetActive(true);
		
	}

	public void activateUserInput(){
		userInput.SetActive(true);
	}

	public void disableUserInput(){
		userInput.SetActive(false);
	}

	public void disableTextViews(){
		acornImage.gameObject.SetActive(false);
		
		scoreText.gameObject.SetActive(false);
		acornCountText.gameObject.SetActive(false);
	}

	public void disableStartText(){
		startText.gameObject.SetActive(false);
	}

	public void activatePowerUpButtons(PowerUp[] powerups){
		for(int i = 0; i < powerups.Length; i++){
			powerUpButtons[i].gameObject.SetActive(true);
			powerUpButtons[i].setButtonIndexValue(i);
			powerUpButtons[i].GetComponent<Image>().sprite = powerups[i].getDisplayImage();
		}
	}

	public void disablePowerUpButtons(){
		for(int i = 0; i < powerUpButtons.Length; i++){
			powerUpButtons[i].gameObject.SetActive(false);
		}
	}

	public void updatePowerUpButtons(PowerUp[] powerups, int acorns){
		for(int i = 0; i < powerups.Length; i++){
			if(powerups[i].getCost() > acorns){
				powerUpButtons[i].GetComponent<Image>().color = new Color(1f,1f,1f,.5f);
			}
			else{
				powerUpButtons[i].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
			}
		}
	}

	public void hideOptionsButton(){
		optionsButton.SetActive(false);
	}

	public void activatePowerUpTimer(PowerUp powerUp){
		powerUpTimer.startTimer(powerUp);
	}

	public void disablePowerUpTimer(){
		powerUpTimer.disable();
	}

	public void disableUIViews(){
		disableTextViews();
		disableUserInput();
		disablePowerUpButtons();
		disablePowerUpTimer();
	}

}
