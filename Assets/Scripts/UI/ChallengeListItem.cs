using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using bteof.utils;
using Facebook.Unity;

public class ChallengeListItem : MonoBehaviour{
    [SerializeField] TMP_Text userNameText;
    [SerializeField] TMP_Text highScoreText;

    [SerializeField] Image profileImage;

    private FBChallenge challengeData = null;

    private  void setUserName(string name){
        userNameText.text = name;
    }

    private void setScore(string score){
        highScoreText.text = score;
    }

    public void setChallengeData(FBChallenge challenge){
        this.challengeData = challenge;
    }

    public void load(){
        setUserName(challengeData.fromName);
        setScore(challengeData.score.ToString());
        downloadProfilePic(challengeData.fromId);
        FindObjectOfType<ChallengeMenuController>().addChallenge();
    }

    private void downloadProfilePic(string user_id){
        FindObjectOfType<FacebookScript>().downloadProfilePic(user_id, delegate (IGraphResult result){
            if(result.Texture){
                profileImage.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 142 , 142), new Vector2());
            }
        });
    }

    public void deleteChallenge(){
        FindObjectOfType<FacebookScript>().deleteRequestForId(challengeData.requestID, delegate (IGraphResult result){
            FindObjectOfType<ChallengeMenuController>().removeChallenge();
            Destroy(gameObject);
        });
    }

}
