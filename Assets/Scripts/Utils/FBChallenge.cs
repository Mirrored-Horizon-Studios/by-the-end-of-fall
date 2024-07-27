using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bteof.utils{
    public class FBChallenge{
        public FBChallenge(){

        }

        public string requestID {get; set;}
        public string fromId {get; set;}
        public string fromName {get; set;}
        public int score {get; set;}

        public string print(){
            string outStr = "";

            outStr += "Request_id: " + requestID + "\n";
            outStr += "From_id: " + fromId + "\n";
            outStr += "From_name: " + fromName + "\n";
            outStr += "Score: " + score + "\n";

            return outStr;
        }

    }

}
