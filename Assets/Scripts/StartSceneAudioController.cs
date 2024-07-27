using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneAudioController : MonoBehaviour{
    private AudioSource music;
    private GameSettings gameSettings;
    void Start(){
        music = gameObject.GetComponent<AudioSource>();
        gameSettings = FindObjectOfType<GameSettings>();	
    }

    // Update is called once per frame
    void Update(){
        music.volume = FindObjectOfType<GameSettings>().getMusicVolume();
    }
}
