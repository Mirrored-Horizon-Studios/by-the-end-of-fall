using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class HunterBody : MonoBehaviour {

	[SerializeField] SpriteMeshInstance headMesh, bodyMesh, tailMesh, frontLegRightMesh, frontLegLefttMesh, backLegLeftMesh, backLegRightMesh;
	[SerializeField] SpriteMesh headDefault, bodyDefault, tailDefault, frontLegRightDefault,frontLegLeftDefault, backLegRightDefault, backLegLeftDefault;

	[SerializeField] SpriteMesh headStone, bodyStone, tailStone, frontLegRightStone, frontLegLeftStone, backLegRightStone, backLegLeftStone;

	[SerializeField] SpriteMesh headLeaf, bodyLeaf, tailLeaf, frontLegRightLeaf, frontLegLeftLeaf, backLegRightLeaf, backLegLeftLeaf;
	
	private Animator hunterAnimator;

	// Use this for initialization
	void Start () {
		hunterAnimator = GetComponent<Animator>();
		FindObjectOfType<Player>().PlayerDied += OnPlayerDied;
		FindObjectOfType<GameSession>().PowerUpStarted += OnPowerUpStarted;
		FindObjectOfType<GameSession>().PowerUpFinished += OnPowerUpFinished;
	}
	
	void OnPowerUpStarted(PowerUp power){
		if(power.getName() == "Stone Squirrel"){
				transformToStone();
		}
	}

	void OnPowerUpFinished(){
		transformToNormal();
	}

	public void startRunning(){
		hunterAnimator.SetBool("gameStarted", true);
	}

	public void setJumpStarted(bool jumpStarted){
		hunterAnimator.SetBool("jumpStarted", jumpStarted);	
	}
	
	public void updateYVelocity(float yVelocity){
		hunterAnimator.SetFloat("yVelocity", yVelocity);
	}

	public void updateIsGrounded(bool isGrounded){
		hunterAnimator.SetBool("isGrounded", isGrounded);
	}

	public void OnPlayerDied(){
		transformToLeaf();
	}

	public void transformToStone(){
		headMesh.spriteMesh = headStone;
		bodyMesh.spriteMesh = bodyStone;
		tailMesh.spriteMesh = tailStone;
		frontLegRightMesh.spriteMesh = frontLegRightStone;
		frontLegLefttMesh.spriteMesh = frontLegLeftStone;
		backLegRightMesh.spriteMesh = backLegRightStone;
		backLegLeftMesh.spriteMesh = backLegLeftStone;
	}

	public void transformToNormal(){
		headMesh.spriteMesh = headDefault;
		bodyMesh.spriteMesh = bodyDefault;
		tailMesh.spriteMesh = tailDefault;
		frontLegRightMesh.spriteMesh = frontLegRightDefault;
		frontLegLefttMesh.spriteMesh = frontLegLeftDefault;
		backLegRightMesh.spriteMesh = backLegRightDefault;
		backLegLeftMesh.spriteMesh = backLegLeftDefault;
	}

	public void transformToLeaf(){
		headMesh.spriteMesh = headLeaf;
		bodyMesh.spriteMesh = bodyLeaf;
		tailMesh.spriteMesh = tailLeaf;
		frontLegRightMesh.spriteMesh = frontLegRightLeaf;
		frontLegLefttMesh.spriteMesh = frontLegLeftLeaf;
		backLegRightMesh.spriteMesh = backLegRightLeaf;
		backLegLeftMesh.spriteMesh = backLegLeftLeaf;
	}
}
