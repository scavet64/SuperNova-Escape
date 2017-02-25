using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour {

    public static GameController control;

    public float bestDistance { get; set; }             //players best distance traveled
    public bool isMuted { get; set; }                   //if the player has the game muted or not
    public int lastUsedQualitySetting { get; set; }		//best quality setting that was determined on initial launch

    public int timesPlayedToday { get; set; }

    // Use this for initialization
    void Awake() {
        if (control != null) {
            Destroy(gameObject);
        } else {
            load();                                                     	//load the players old information into datafields
            control = this;                                                 //put this object into the static control field
            GameObject.DontDestroyOnLoad(gameObject);                       //ensure this is the only instance of this game object
            //adManager = AdvertManager.adManager;                          //store reference to singleton admanager
            //audioSource.mute = isMuted;                                   //set the audiosource to its correct muted state
            timesPlayedToday = 0;                                           //initialize the number of plays this session to 0
                                                                            //Debug.Log (Application.persistentDataPath);
            QualitySettings.SetQualityLevel(lastUsedQualitySetting, true);  //set the quality setting using last quality setting
                                                                            //unlockAllSkins ();
            Application.targetFrameRate = 60;
            Social.localUser.Authenticate(ProcessAuthentication);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    // This function gets called when Authenticate completes
    // Note that if the operation is successful, Social.localUser will contain data from the server. 
    void ProcessAuthentication(bool success) {
        if (success) {
            Debug.Log("Authenticated, checking achievements");

            // Request loaded achievements, and register a callback for processing them
            Social.LoadAchievements(ProcessLoadedAchievements);
        } else
            Debug.Log("Failed to authenticate");
    }

    // This function gets called when the LoadAchievement call completes
    void ProcessLoadedAchievements(IAchievement[] achievements) {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            Debug.Log("Got " + achievements.Length + " achievements");

        // You can also call into the functions like this
        Social.ReportProgress("Achievement01", 100.0, result => {
            if (result)
                Debug.Log("Successfully reported achievement progress");
            else
                Debug.Log("Failed to report achievement");
        });
    }

    public void showLeaderboard() {
        Social.ShowLeaderboardUI();
    }

    public void save() {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.bestDistance = bestDistance;
        data.isMuted = isMuted;
        data.lastUsedQualitySetting = QualitySettings.GetQualityLevel();

        bf.Serialize(file, data);
        file.Close();
    }

    void load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = bf.Deserialize(file) as PlayerData;
            file.Close();

            bestDistance = data.bestDistance;
            isMuted = data.isMuted;
            lastUsedQualitySetting = data.lastUsedQualitySetting;
        } else {
            bestDistance = 0f;
            isMuted = false;
            lastUsedQualitySetting = 0;
        }
    }

    [Serializable]
    class PlayerData {
        public float bestDistance;
        public bool isMuted;
        public int lastUsedQualitySetting;
    }
}
