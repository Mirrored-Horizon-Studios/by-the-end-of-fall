using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(GameSettings))]
public class OptionsMenuController : MonoBehaviour{

[SerializeField] Slider musicVolume, fxVolume;

private GameSettings gameSettings;

    // Start is called before the first frame update
    void Start(){
        gameSettings = FindObjectOfType<GameSettings>();

        //set default levels
        musicVolume.value = gameSettings.getMusicVolume();
        fxVolume.value = gameSettings.getFXVolume();

        musicVolume.onValueChanged.AddListener(delegate {musicVolumeAdjusted(); });
        fxVolume.onValueChanged.AddListener(delegate {fxVolumeAdjusted();});
    }    

    public void musicVolumeAdjusted(){
        gameSettings.updateMusicVolume(musicVolume.value);
    }

    public void fxVolumeAdjusted(){
        gameSettings.updateFXVolume(fxVolume.value);
    }

    public void open(){
        if(!gameObject.activeInHierarchy){
            gameObject.SetActive(true);
        }
    }

    public void close(){
        if(gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }
    }
}
