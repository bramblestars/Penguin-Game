using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using System;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    [SerializeField] private TextMeshProUGUI extraMessage;

    private string publicLeaderboardKey = 
        "6de4fe3638406fa80ccec6d32e18ae4ac29c9816934bbb9b071dbde5470ddfbf";

    private void Start() 
    {
        GetLeaderboard();
    }

    public void GetLeaderboard() 
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            int loopLength = Math.Min(msg.Length, names.Count);
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        extraMessage.enabled = true;
        if (username == "") 
        {
            extraMessage.text = "enter a username!";
            return;
        }

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, 
          score, ((msg) => {
            GetLeaderboard();
          }));
        
        extraMessage.text = "score submitted!";

        GetLeaderboard();

    }

}
