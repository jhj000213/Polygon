using UnityEngine;
using System.Collections;
using GooglePlayGames;
public class ReaderBoardScoreSet : MonoBehaviour {

    public void ShowBoard()
    {
        Debug.Log("ShowLeaderBoard");
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    //갱신
    public void SetScore_ReaderBoard()
    {
        int score = PlayerPrefs.GetInt("score");
        if (score > 0)
        {
            Social.ReportScore(score, Polygon.RankingScene_Google.leaderboard_ranking, success =>
            {
                Debug.Log("report_score");
            });
        }
    }
}