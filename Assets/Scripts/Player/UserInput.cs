using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour{
    [SerializeField] Player player;

    void OnMouseDown(){
		if(player.isGrounded){
			player.Jump();
		}
	}
}
