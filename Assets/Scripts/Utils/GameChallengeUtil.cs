using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bteof.utils{

    public class GameChallengeUtil{

        public static List<bteof.utils.FBChallenge> parseValidGameRequests(List<object> challenges, out Queue<string> invalidRequests){
            var validChallenges = new List<FBChallenge>();
            invalidRequests = new Queue<string>();

            foreach(var challenge in challenges){
                var challengeDict = ((Dictionary<string, object>)challenge);
                        
                string requestId = challengeDict["id"].ToString();
                var requestFrom = ((Dictionary<string, object>)challengeDict["from"]);
                int score = 0;

                if(challengeDict.ContainsKey("data") && int.TryParse(challengeDict["data"].ToString(), out score)){
                    var fbChallenge = new bteof.utils.FBChallenge();
                    
                    fbChallenge.requestID = requestId;
                    fbChallenge.fromId = requestFrom["id"].ToString();
                    fbChallenge.fromName = requestFrom["name"].ToString();
                    fbChallenge.score = score;

                    validChallenges.Add(fbChallenge);
                }      
                else{
                    invalidRequests.Enqueue(requestId);
                }
            }

        return validChallenges;  
        }
    }
}
