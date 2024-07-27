using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
	// Configuration Params
	[Header("Appearance And Collision")]
	[SerializeField] HunterBody body;
	[SerializeField] CapsuleCollider2D bodyCollider;
	[SerializeField] CapsuleCollider2D groundCollider;

	[Header("Map Generation")]
	[SerializeField] MapGenerator mapLoader;
	[SerializeField] int safeZoneEndPos;

	[Header("Player Movement")]
	[SerializeField] float moveSpeed = 1f;
	[SerializeField] float jumpHeight = 3f;
	[SerializeField] float slideDuration = 1f;

	[Header("Sound FX")]
	[SerializeField] AudioClip deathSound;
	[Range(0,1)][SerializeField] float soundFXVolume = .5f;

	[Header("VFX")]
	[SerializeField] GameObject deathVFX;

	public delegate void PlayerDiedHandler ();
	public event PlayerDiedHandler PlayerDied;
	
	Rigidbody2D playerBody;
	GameSession gameSession;
	

	//State Params
	private float activeSpeed = 0;
	public bool isGrounded = true;
	bool isSliding = false;
	private float lastXPos;

	private bool died = false;

	private GameSettings gameSettings;
	private AudioSource fxAudioSource; 


	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
		gameSession = FindObjectOfType<GameSession>();
		gameSettings = FindObjectOfType<GameSettings>();
		fxAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		if(died == false){
			LoadNextMapFragment();

			manageFXVolume();
			manageSoundFX();

			Run();

			if(Input.GetButtonDown("Jump") && isGrounded){			
					Jump();
			}	
			
			applyPowerUp();

			body.updateYVelocity(playerBody.velocity.y);	
		}
		else{
			playerBody.velocity = new Vector2(0,0);
		}
	}
	
	void Run(){
		playerBody.velocity = new Vector2(1 * activeSpeed, playerBody.velocity.y);

		lastXPos = playerBody.transform.position.x;
		FindObjectOfType<GameSession>().SetDistanceTraveled(GetUnitsTraveled());
	}

	public void Jump(){
		if(isGrounded){
			body.setJumpStarted(true);
			playerBody.velocity = new Vector2(0, jumpHeight);
		}
	}

	void LoadNextMapFragment(){
		if(Vector2.Distance(transform.position, mapLoader.getLastGroundPosition()) < 20){
			mapLoader.generateSegment();
		}
	}

	public void SetPlayerGrounded(bool grounded){
		if(isGrounded == false && grounded == true){
			body.setJumpStarted(false);
			if(gameSession.getActivePowerup() != null && gameSession.getActivePowerup().getName() == "Flying Squirrel"){
				gameSession.stopActivePowerUp();
			}
		}

		isGrounded = grounded;
		
		body.updateIsGrounded(grounded);
	}

	private void applyPowerUp(){
		if(gameSession.getActivePowerup() != null){
			if(gameSession.getActivePowerup().getName() == "Flying Squirrel"){
				playerBody.velocity = new Vector2(playerBody.velocity.x, -0.3f);
			}
			else if(gameSession.getActivePowerup().getName() == "Stone Squirrel"){
				playerBody.gravityScale = 4;
			}
		}
		else{
			playerBody.gravityScale = 1;
		}
	}

	private void manageSoundFX(){
		if(isGrounded && activeSpeed > 0 && gameSession.gameIsPaused() == false){
			fxAudioSource.mute = false;
		}
		else if(gameSession.gameIsPaused()){
			fxAudioSource.mute = true;
		}
		else{
			fxAudioSource.mute = true;
		}
		
	}

	public void startMoving(){
		activeSpeed = moveSpeed;
		body.startRunning();
		fxAudioSource.mute = false;
	}

	public void stopMoving(){
		activeSpeed = 0;
	}

	private int GetUnitsTraveled(){
		Vector2 start = new Vector2(safeZoneEndPos, 0);
		Vector2 playerPos = new Vector2(transform.position.x, 0);
		int distance = Mathf.RoundToInt(Vector2.Distance(start, playerPos));
		return distance;
	}

	public void die(){
		if(!died){
			//dont let hunter die twice if multiple death colissions take place.
			//Stop hunter from moving
			died = true;

			fxAudioSource.mute = true;
			AudioSource.PlayClipAtPoint(deathSound,  Camera.main.transform.position, gameSettings.getFXVolume());
			

			doDieEffect();
			PlayerDied.Invoke();

		}
	}

	public void doDieEffect(){
		var explode = Instantiate(deathVFX, transform.position, Quaternion.identity);
		Destroy(body.gameObject, .2f);		
	}

	private void manageFXVolume(){
		fxAudioSource.volume = gameSettings.getFXVolume();
	}
}
