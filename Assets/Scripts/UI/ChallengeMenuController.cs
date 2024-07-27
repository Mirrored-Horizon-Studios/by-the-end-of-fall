using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using bteof.utils;

public class ChallengeMenuController : MonoBehaviour{
    [Header("Challenge Alerts")]
    [SerializeField] TMP_Text challengeCountText;

    [Header("Challenge Menu")]
    [SerializeField] GameObject challengeMenuPanel;
    [SerializeField] GameObject contentPanel;
    [SerializeField] GameObject listItemPrefab;

    [SerializeField] TMP_Text highScoreText;

    private int challengeCount = 0;

    // Start is called before the first frame update
    void Start(){
        FacebookScript Fb = FindObjectOfType<FacebookScript>();
        Fb.ChallengesLoaded += onChallengesLoaded;

        SetHighScoreText();
    }

    void Update(){
        challengeCountText.text = challengeCount.ToString();
    }

    private void onChallengesLoaded(List<FBChallenge> facebookChallenges){

        foreach(FBChallenge challenge in facebookChallenges){
            GameObject listItem = Instantiate(listItemPrefab);
            var itemController = listItem.GetComponent<ChallengeListItem>();
            
            itemController.setChallengeData(challenge);
            itemController.load();

            listItem.transform.SetParent(contentPanel.transform, false);
        }
    }

    public void openChallengeMenu(){
        challengeMenuPanel.SetActive(true);
    }

    public void closeChallengeMenu(){
        challengeMenuPanel.SetActive(false);
    }

     private void SetHighScoreText(){
        if (PlayerPrefs.HasKey("high_score")){
            highScoreText.text = PlayerPrefs.GetInt("high_score").ToString();
        }
        else{
            highScoreText.text = "0";
        }
    }

    public void addChallenge(){
        challengeCount++;
    }

    public void removeChallenge(){
        challengeCount--;
    }

}
