using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GameCenterController : MonoBehaviour {

    private static readonly string leaderboardID = "grp.7b8ac33ada8b4a748ef3cb7cc71377f6";

    // Use this for initialization
    void Start () {
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    public static void ProcessAuthentication(bool success) {
        if (success) {
            Debug.Log("Authenticated, checking achievements");

            // Request loaded achievements, and register a callback for processing them
            Social.LoadAchievements(ProcessLoadedAchievements);
        } else
            Debug.Log("Failed to authenticate");
    }

    // This function gets called when the LoadAchievement call completes
    public static void ProcessLoadedAchievements(IAchievement[] achievements) {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");
    }

    public static void reportProgressForAchievement(string id, double percentComplete) {

        percentComplete *= 100;

        if (percentComplete > 100) {
            percentComplete = 100;
        }

        Social.ReportProgress(id, percentComplete, result => {
        if (result)
            Debug.Log("Successfully reported achievement progress");
        else
            Debug.Log("Failed to report achievement");
        });
    }

    /// <summary>
    /// This method displays the built in apple leaderboard for game center.
    /// </summary>
    public static void showLeaderboard() {
        GameCenterPlatform.ShowLeaderboardUI(leaderboardID, TimeScope.AllTime);
    }

    /// <summary>
    /// shows the achievementUI
    /// </summary>
    public static void ShowAchievements() {
        Social.ShowAchievementsUI();
    }

    /// <summary>
    /// Method will save the passed in score to my hosted database as a backup
    /// </summary>
    /// <param name="score">integer representing the players score</param>
    public static void saveScoreToMyDatabase(int score) {
        //TODO: Implement method
    }

    /// <summary>
    /// Persist the score to the game center database. This will be the primary method of persisting scores.
    /// </summary>
    /// <param name="score">integer representing the players score</param>
    public static void PersistScoreToGamecenterDatabase(int score) {
        Social.ReportScore(score, leaderboardID, result => {
            if (result)
                Debug.Log("Successfully reported score");
            else
                Debug.Log("Failed to report score");
        });
    }
}
