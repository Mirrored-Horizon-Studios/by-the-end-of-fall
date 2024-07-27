using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTimer : MonoBehaviour
{
    Slider timeSlider;
    bool active = false;

    float duration;

    void Update(){
        if(active){
            duration -= Time.deltaTime;

            setValue(duration);
        }
    }

    public void setPowerUpSprite(Sprite powerUpSprite){
        Slider timeSlider =  GetComponent<Slider>();
      GameObject handle = timeSlider.handleRect.gameObject;

       handle.GetComponent<Image>().sprite = powerUpSprite;
    }

    public void setValue(float time){
        Slider timeSlider =  GetComponent<Slider>();
        timeSlider.value = time;
    }

    private void setTimerDuration(float powerUpDuration){
        Slider timeSlider =  GetComponent<Slider>();
        
        duration = powerUpDuration;

        timeSlider.maxValue = duration;
        setValue(duration);
    }

    public void enable(){
        GetComponent<Slider>().gameObject.SetActive(true);
    }

    public void disable(){
        GetComponent<Slider>().gameObject.SetActive(false);
    }

    public void startTimer(PowerUp powerUp){
        setPowerUpSprite(powerUp.getDisplayImage());
        setTimerDuration(powerUp.getDuration());
        active = true;
        enable();
    }
}
