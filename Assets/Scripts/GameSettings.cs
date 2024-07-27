using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private float musicVolume = .5f;
    private float fxVolume = 1;

    private const string MUSIC_VOLUME = "music_volume";
    private const string FX_VOLUME = "fx_volume";
    private const string HIDE_TUTORIAL = "hide_tutorial";

    // Start is called before the first frame update
    void Start(){        
        if(PlayerPrefs.HasKey(MUSIC_VOLUME) == false){
            PlayerPrefs.SetFloat(MUSIC_VOLUME, musicVolume);
        }
        if(PlayerPrefs.HasKey(FX_VOLUME) == false){
            PlayerPrefs.SetFloat(FX_VOLUME, fxVolume);
        }
    }
    public void resetTutorial(){
        PlayerPrefs.SetInt(HIDE_TUTORIAL, 0);
    }

    public float getMusicVolume(){
        return PlayerPrefs.GetFloat(MUSIC_VOLUME);
    }

    public float getFXVolume(){
        return PlayerPrefs.GetFloat(FX_VOLUME);
    }

    public void updateMusicVolume(float volume){
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
    }

    public void updateFXVolume(float volume){
        PlayerPrefs.SetFloat(FX_VOLUME, volume);
    }

    public void toggleHideTutorial(){
        if(getShouldHideTutorial()){
            PlayerPrefs.SetInt(HIDE_TUTORIAL, 0);
        }
        else{
            PlayerPrefs.SetInt(HIDE_TUTORIAL, 1);
        }
    }

    public bool getShouldHideTutorial(){
        if(PlayerPrefs.HasKey(HIDE_TUTORIAL)){
            return PlayerPrefs.GetInt(HIDE_TUTORIAL) == 1;
        }
        
        return false;
    }
}
