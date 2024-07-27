using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildNumber : MonoBehaviour{
    [SerializeField] Text textView;

    // Start is called before the first frame update
    void Start(){
        textView.text = "Version " + Application.version.ToString();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
