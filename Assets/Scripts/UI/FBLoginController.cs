using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FBLoginController : MonoBehaviour{
    [SerializeField] Image profilePic;
    [SerializeField] Sprite profileDefaultImage;
    [SerializeField] Button loginButton;
    [SerializeField] TMP_Text loginButtonText;

    private FacebookScript Fb;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start(){
        Fb = FindObjectOfType<FacebookScript>();

        Fb.LoggedIn += onLoggedIn;
        Fb.LoggedOut += onLoggedOut;
    }

    public void onLoggedIn(){
        loginButtonText.text = "Logout";
        profilePic.sprite = Sprite.Create(Fb.profile_pic, new Rect(0, 0, 128, 128), new Vector2());
    }

    public void onLoggedOut(){
        resetLoginViews();
    }

    private void resetLoginViews(){
        profilePic.sprite = profileDefaultImage;
        loginButton.gameObject.SetActive(true);
        loginButtonText.text = "Login";
    }

}
