using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrivacyPolicy : MonoBehaviour{
    [SerializeField] GameObject privacyPolicyContainer;
    [SerializeField] Toggle toggle;

    const string PRIVACY_POLICY_KEY = "privacy_policy";
    // Start is called before the first frame update
    void Start() {
        if(!ppAccepted()){
            privacyPolicyContainer.SetActive(true);
        }else{
            privacyPolicyContainer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    private bool ppAccepted(){
        if(PlayerPrefs.GetInt(PRIVACY_POLICY_KEY) == 1){
            return true;
        }

        return false;
    }

    public void toggleClicked(){
        if(!toggle.isOn){
            PlayerPrefs.SetInt(PRIVACY_POLICY_KEY, 0);
        }else{
            PlayerPrefs.SetInt(PRIVACY_POLICY_KEY, 1);
        }

        PlayerPrefs.Save();
    }

    public void closeButtonClicked(){
        if(ppAccepted()){
            privacyPolicyContainer.SetActive(false);
        }
    }
}
