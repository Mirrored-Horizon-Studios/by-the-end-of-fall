using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour{
    [SerializeField] GameObject[] slides;

    public delegate void OnTutorialComplete();
    private  event OnTutorialComplete onTutorialComplete;

    // Start is called before the first frame update
    void Start(){
        //FindObjectOfType<GUIController>().disableUserInput();
        activateSlide(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addOnTutorialCompleteObserver(OnTutorialComplete handler){
        onTutorialComplete += handler;
    }

    private void activateSlide(int index){
        deactivateAllSlides();

        if(index >= slides.Length){
            Debug.Log("Slide index out of range.");
            return;
        }

        slides[index].SetActive(true);
    }

    private void deactivateAllSlides(){
        foreach(GameObject slide in slides){
            slide.SetActive(false);
        }
    }

     /// <summary>
     /// Moves to the next slide, if on the last slide will disable all slide menus.
     /// </summary>
     /// <param name="currentSlideIndex">The index value of the current slide. Not the index of the next slide.</param>
    public void moveToNextSlide(int currentSlideIndex){
        currentSlideIndex++;

        if(currentSlideIndex < slides.Length){
            activateSlide(currentSlideIndex);
        }
        else{
            deactivateAllSlides();
            onTutorialComplete();
        }
    }
}
