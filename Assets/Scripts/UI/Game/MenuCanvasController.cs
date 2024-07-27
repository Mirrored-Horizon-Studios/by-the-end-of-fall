using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCanvasController : MonoBehaviour{
    [SerializeField] GameSession gameSession;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject tutorialPanel;

    // Start is called before the first frame update
    void Start(){

        if(FindObjectOfType<GameSettings>().getShouldHideTutorial() == false){
            openTutorialSlide();
            tutorialPanel.GetComponent<TutorialController>().addOnTutorialCompleteObserver(onTutorialComplete);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTutorialComplete(){
        closeTutorialSlide();
    }

    public bool windowShowing(){
        return(optionsPanel.activeInHierarchy || tutorialPanel.activeInHierarchy);
    }

    private void openOptionsMenu(){
		gameSession.pauseGame();
		optionsPanel.SetActive(true);
	}

	private void closeOptionsMenu(){
		gameSession.resumeGame();
		optionsPanel.SetActive(false);
	}

	public void toggleOptionsMenu(){
        if(!tutorialPanel.activeInHierarchy && gameSession.gameHasEnded == false){
            if(optionsPanel.activeInHierarchy){
                closeOptionsMenu();
            }
            else{
                openOptionsMenu();
            }
        }
	}

    public void openTutorialSlide(){
        tutorialPanel.SetActive(true);
    }

    public void closeTutorialSlide(){
        tutorialPanel.SetActive(false);
    }
}
