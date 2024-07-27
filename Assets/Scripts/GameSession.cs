using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameSession : MonoBehaviour {
	//Configuration Vars
	[SerializeField] GameSettings gameSettings;

	[Header("UI Elements")]
	[SerializeField] ScoreBoardController scoreBoardUI;
	[SerializeField] GUIController gameUIController;
	[SerializeField] MenuCanvasController menuCanvasController;

	[Header("Script Config")]
	[SerializeField] int distanceToScoreFactor = 1;
	[SerializeField] Background[] scrollingBackgrounds;
	[SerializeField] PowerUp[] powerups;

	private const string rewardAcornsKey = "REWARD_ACORNS";

	//State Vars
	private Player player;
	private AcornDropper acornDropper;
	PowerUp activePowerUp;
	private float powerUpCountDown;
	private bool hasStarted = false;
	private bool hasEnded = false;
	private int acorns = 0;	
	int score;

	public delegate void PowerUpStartedHandler(PowerUp activePowerUp);
	public delegate void PowerUpFinishedHandler();

	public event PowerUpStartedHandler PowerUpStarted;
	public event PowerUpFinishedHandler PowerUpFinished;

	private bool gamePaused = false;
	
	void Start () {
		player = FindObjectOfType<Player>();
		player.PlayerDied += onPlayerDied;
		acornDropper = FindObjectOfType<AcornDropper>();
	}
	
	void Update () {

		adjustAudio();

		if(hasEnded == false){

			if(menuCanvasController.windowShowing()){
					gameUIController.disableStartText();
			}
			
			if(Input.GetKeyUp(KeyCode.Mouse0)){
				if(hasStarted == false && menuCanvasController.windowShowing() == false){
					startGame();
				}
			}

			gameUIController.updatePowerUpButtons(powerups, acorns);
			updateActivePowerupDuration();

			gameUIController.updateScoreText(score.ToString());
			gameUIController.updateAcornText(acorns.ToString());

			saveHighScore();	
		}

	}

	public void onPlayerDied(){
		hasEnded = true;
		
		StartCoroutine(stopGame());
	}


	// Game LifeCycle
	private void startGame(){
		if(hasEnded == false){
			
			gameUIController.disableStartText();

			if(PlayerPrefs.HasKey(rewardAcornsKey)){
				acorns += PlayerPrefs.GetInt(rewardAcornsKey);
				PlayerPrefs.SetInt(rewardAcornsKey, 0);
				PlayerPrefs.Save();
			}	

			activateUIViews();

			player.startMoving();
			gameUIController.activateUserInput();

			for(int i = 0; i < scrollingBackgrounds.Length; i++){
				scrollingBackgrounds[i].StartScrolling();
			}

			acornDropper.startSpawning();

			hasStarted = true;
		}
	}

	private IEnumerator stopGame(){
			player.stopMoving();
			gameUIController.disableUserInput();
			gameUIController.disableUIViews();
			gameUIController.hideOptionsButton();
			gameUIController.disablePowerUpTimer();
			acornDropper.stopSpawning();

			for(int i = 0; i < scrollingBackgrounds.Length; i++){
				scrollingBackgrounds[i].StopScrolling();
			}

			yield return new WaitForSeconds(2f);		

			openScoreMenu(); 			
	}

	// Score State
	public void SetDistanceTraveled(int distance){this.score = distance * distanceToScoreFactor;}

	public void AddAcorn(){
		acorns++;
	}

	//Power Up Management
	public void activatePowerUp(int powerUpIndex){
		if(powerups[powerUpIndex] == activePowerUp){
			activePowerUp = null;
			PowerUpFinished.Invoke();
			gameUIController.disablePowerUpTimer();
		}

		else if(powerups[powerUpIndex].getCost() <= acorns && powerUpActivationConditionsAreMet(powerups[powerUpIndex])){		
			activePowerUp = powerups[powerUpIndex];
			gameUIController.activatePowerUpTimer(activePowerUp);

			PowerUpStarted.Invoke(activePowerUp);
			powerUpCountDown = activePowerUp.getDuration();
			acorns -= activePowerUp.getCost();	

		}
	}

	private void updateActivePowerupDuration(){
		if(activePowerUp != null){
			powerUpCountDown -= Time.deltaTime;

			if(powerUpCountDown <= 0){
				activePowerUp = null;
				PowerUpFinished.Invoke();
				gameUIController.disablePowerUpTimer();
			}
		}
	}

	public void stopActivePowerUp(){
		activePowerUp = null;
		PowerUpFinished.Invoke();
	}

	public PowerUp getActivePowerup(){
		return activePowerUp;
	}

	public bool powerUpActivationConditionsAreMet(PowerUp selectedPowerup){
		if(selectedPowerup.getName() == "Flying Squirrel" && player.isGrounded){
			return false;
		}

		return true;
	}

	//Main Game UI Control
	private void activateUIViews(){
		gameUIController.activateTextViews();
		gameUIController.activatePowerUpButtons(powerups);
		gameUIController.activateUserInput();
	}
	//Score Board Control
	private void saveHighScore(){
		if(PlayerPrefs.HasKey("high_score")){
			int currentHighScore = PlayerPrefs.GetInt("high_score");

			if(score > currentHighScore){
				PlayerPrefs.SetInt("high_score", score);
			}
		}
		else{
			//No high score saved, save first score as high score
			PlayerPrefs.SetInt("high_score", score);
		}
	}

	public void openScoreMenu(){
		scoreBoardUI.setCurrentScore(score.ToString());

		if(PlayerPrefs.HasKey("high_score")){
			scoreBoardUI.setHighScoreText(PlayerPrefs.GetInt("high_score").ToString());
		}
		else{
			scoreBoardUI.setHighScoreText(score.ToString());
		}
		scoreBoardUI.open();
	}

	public void pauseGame(){
		gamePaused = true;
		gameUIController.disableUserInput();
		Time.timeScale = 0;
	}

	public void resumeGame(){
		gamePaused = false;
		gameUIController.activateUserInput();
		Time.timeScale = 1;
	}

	public bool gameIsPaused(){
		return gamePaused;
	}

	public bool gameHasStarted{
		get{
			return hasStarted;
		}
	}

	public bool gameHasEnded{
		get{
			return hasEnded;
		}
	}

	private void adjustAudio(){
		GetComponent<AudioSource>().volume = gameSettings.getMusicVolume();
	}
}
