using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    private string publicLeaderboardKey = 
        "6de4fe3638406fa80ccec6d32e18ae4ac29c9816934bbb9b071dbde5470ddfbf";

    public void GetLeaderboard() 
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) => {
            for (int i = 0; i < names.Count; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, 
          score, ((msg) => {
            GetLeaderboard();
          }));
    }

}
