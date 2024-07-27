using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour{
    private RewardBasedVideoAd rewardBasedVideoAd;
    private const string rewardAcornsKey = "REWARD_ACORNS";

    void Start(){
        MobileAds.Initialize("ca-app-pub-7006334799435054~4271985994");

        this.rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        rewardBasedVideoAd.OnAdOpening += HandleOnAdOpening;
        rewardBasedVideoAd.OnAdStarted += HandleOnAdStarted;
        rewardBasedVideoAd.OnAdClosed += HandleOnAdClosed;
        rewardBasedVideoAd.OnAdRewarded += HandleOnAdRewarded;
        rewardBasedVideoAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        rewardBasedVideoAd.OnAdCompleted += HandleOnAdCompleted;
        rewardBasedVideoAd.OnAdFailedToLoad += HandleHandleOnAdFailedToLoad;

        loadRewardBasedAd();

    }

    void OnDestroy(){
        //Remove handlers otherwise they accumulate over time.
        rewardBasedVideoAd.OnAdOpening -= HandleOnAdOpening;
        rewardBasedVideoAd.OnAdStarted -= HandleOnAdStarted;
        rewardBasedVideoAd.OnAdClosed -= HandleOnAdClosed;
        rewardBasedVideoAd.OnAdRewarded -= HandleOnAdRewarded;
        rewardBasedVideoAd.OnAdLeavingApplication -= HandleOnAdLeavingApplication;
        rewardBasedVideoAd.OnAdCompleted -= HandleOnAdCompleted;
        rewardBasedVideoAd.OnAdFailedToLoad -= HandleHandleOnAdFailedToLoad;   
    }

    // Update is called once per frame
    void Update(){
        
    }

    public Boolean adIsLoaded(){
        return rewardBasedVideoAd.IsLoaded();
    }

    public void loadRewardBasedAd(){
        #if UNITY_EDITOR
            string addUnitId = "unused";
        #elif UNITY_ANDROID
            string addUnitId = "ca-app-pub-7006334799435054/5109335575";
            //test
            //string addUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
            string addUnitId = "";
        #else
            string addUnitID = "unexpected_platform"
        #endif

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideoAd.LoadAd(request, addUnitId);
    }

    public void showRewardBasedAd(){
        if(rewardBasedVideoAd.IsLoaded()){
            rewardBasedVideoAd.Show();
        }else{
            print("Add not loaded.");
        }

    }


    public void HandleOnAdOpening(System.Object sender, EventArgs args){
    }

    public void HandleOnAdStarted(System.Object sender, EventArgs args){
    }

    public void HandleOnAdClosed(System.Object sender, EventArgs args){
        this.loadRewardBasedAd();
    }

    public void HandleOnAdRewarded(System.Object sender, Reward args){
        print("You get " + args.Amount.ToString() + " acorns!");

        if(!PlayerPrefs.HasKey(rewardAcornsKey)){
            PlayerPrefs.SetInt(rewardAcornsKey, 0);
        }
        else{
            int rewardAcorns = PlayerPrefs.GetInt(rewardAcornsKey);
            rewardAcorns += (int)args.Amount;
            PlayerPrefs.SetInt(rewardAcornsKey, rewardAcorns);
        }

        PlayerPrefs.Save();
    }

    public void HandleOnAdLeavingApplication(System.Object sender, EventArgs args){
    }

    public void HandleOnAdCompleted(System.Object sender, EventArgs args){
    }

    public void HandleHandleOnAdFailedToLoad(System.Object sender, EventArgs args){
        this.loadRewardBasedAd();
    }

    
}
