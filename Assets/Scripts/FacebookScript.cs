using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using TMPro;
using bteof.utils;

public class FacebookScript : MonoBehaviour{
    public Texture2D profile_pic;

    public delegate void OnLoggedIn();
     public delegate void OnLoggedOut();
    public event OnLoggedIn LoggedIn;
    public event OnLoggedOut LoggedOut;

    public delegate void ChallengesReady(List<FBChallenge> facebookChallenges);
    public event ChallengesReady ChallengesLoaded;

    void Awake(){
        if(!FB.IsInitialized){
             FB.Init(onInitComplete: FBInitCallBack);
        }
        else{
            FBInitCallBack();
        } 

    }

     void FBInitCallBack(){
        FB.ActivateApp();

        if (FB.IsLoggedIn) { 
            OnFacebookLogin();
        }
    }

    
    private void FbGetPicture(IGraphResult result) {
        if (result.Texture != null){
            profile_pic = result.Texture;
            LoggedIn.Invoke();
        }
    }

    #region Login / Logout
    public void FacebookLogin(){
        var permissions = new List<string>(){"public_profile", "email", "user_friends"};
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    public void FacebookLogout(){
        FB.LogOut();
        LoggedOut.Invoke();
    }

    public void LogInOut(){
        if(FB.IsLoggedIn){
            FacebookLogout();
        }
        else{
            FacebookLogin();
        }
    }

    private void AuthCallback (ILoginResult result) {
        if (FB.IsLoggedIn) {
            OnFacebookLogin();

        } else {
            Debug.Log("User cancelled login");
        }
    }

    private void OnFacebookLogin(){
        FB.API("/me/apprequests", HttpMethod.GET, OnGameRequestRecieved);
        FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, FbGetPicture); 
    }
    #endregion

    #region Inviting
    public void OnGameRequestRecieved(IGraphResult result){
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);

            if(!dictionary.ContainsKey("data")) return;

            var validChallenges = new List<FBChallenge>();
            var invalidRequests = new Queue<string>();
        
            var requestList = (List<object>)dictionary["data"];
            validChallenges = GameChallengeUtil.parseValidGameRequests(requestList, out invalidRequests);
            
            string dump = "";

            foreach(var challenge in validChallenges){
                dump += challenge.print();
            }

            string invalidRequestId = null;
            while(invalidRequests.Count > 0){
                invalidRequestId = invalidRequests.Dequeue();

                FB.API(invalidRequestId, HttpMethod.DELETE, null);
            }

            ChallengesLoaded.Invoke(validChallenges);
    }

    public void FacebookGameRequest(){
        if(FB.IsLoggedIn){
            print("Game Request Executed.");

            var highScore = PlayerPrefs.GetInt("high_score").ToString();	

            FB.AppRequest(
                title:"By The End Of Fall",
                message:"challenge",
                data:highScore
            );
        }
    }

    public void deleteRequestForId(string requestId, FacebookDelegate<IGraphResult> callback){
        FB.API(requestId, HttpMethod.DELETE, callback);
    }

    public void FacebookInvite(){
        FB.Mobile.AppInvite(new System.Uri("https://play.google.com/store/apps/details?id=com.MirroredHorizonStudios.BytheEndofFall"));
    }

    public void FacebookPost(){
        FB.ShareLink(
            new System.Uri("https://play.google.com/store/apps/details?id=com.MirroredHorizonStudios.BytheEndofFall"),
            callback: null
        );
    }

    public void downloadProfilePic(string user_id, FacebookDelegate<IGraphResult> callback){
         FB.API("https" + "://graph.facebook.com/" + user_id + "/picture?type=square&height=142&width=142", HttpMethod.GET, callback);
    }

    #endregion
}
