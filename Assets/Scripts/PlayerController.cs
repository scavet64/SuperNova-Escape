﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System;

public class PlayerController : MonoBehaviour {

    public static int leftButtonSender = 1;
    public static int rightButtonSender = 2;

    public Text distanceText;

    public GameObject spawnerObject;
    public GameObject cameraObject;
    public GameObject losePanel;
    public GameObject backgroundObject;
    public GameObject endScoreText;
    public GameObject endScoreBest;
    public GameObject inGameGUI;
    public GameObject explosionPrefab;
    public GameObject BackgroundPrefab;
    public AudioClip[] movementSounds;

    //NOTE: If I want score at end of game, divide Y by 2.5

    private Animator cameraAnimator;
    private Animator playerAnimator;
    private Animator losePanelAnimator;
    private Animator backgroundAnimator;
    private Spawner rockSpawner;
    private AudioSource audioSource;
    private bool isShipOnLeft;
    private int score;
    private bool isGameOver;
    private GameController controller;


    // Use this for initialization
    void Start () {

        cameraObject = Camera.main.gameObject;

        Time.timeScale = 1;
        isShipOnLeft = false;
        rockSpawner = spawnerObject.GetComponent<Spawner>();
        playerAnimator = this.gameObject.GetComponent<Animator>();
        cameraAnimator = cameraObject.GetComponent<Animator>();
        losePanelAnimator = losePanel.GetComponent<Animator>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        backgroundAnimator = backgroundObject.GetComponent<Animator>();
        controller = GameController.control;

        this.score = 0;
        isGameOver = false;
        RequestBanner();
        GameController.control.timesPlayedToday++;

    }

    void RequestBanner() {
        try {
            AdvertManager.adManager.RequestBanner();
        } catch (Exception e) {
            Debug.Log(e.StackTrace);
        }
    }

    public void MoveShip(int i) {
        if (IsMoveValid()) {
            
            //determine which animation to play
            if (i == leftButtonSender) {
                if (isShipOnLeft) {
                    //LeftToLeft
                    PlayMoveAnimation("UpWiggleRight");
                } else {
                    //rightToLeft
                    PlayMoveAnimation("RightToLeft");
                    isShipOnLeft = true;
                }
            } else {
                //rightbutton is sender
                if (isShipOnLeft) {
                    //leftToRight
                    PlayMoveAnimation("LeftToRight");
                    isShipOnLeft = false;
                } else {
                    //rightToRight
                    PlayMoveAnimation("UpWiggleLeft");
                }
            }

            PlayMovementAudio();
            rockSpawner.SpawnRocksIfNeeded();
            rockSpawner.SpawnBackgroundIfNeeded();
            IncrementScore();
            MoveCamera();
            MoveBackground();
        }
    }

    private void PlayMovementAudio() {
        audioSource.clip = movementSounds[UnityEngine.Random.Range(0, movementSounds.Length - 1)];
        audioSource.Play();
    }

    public static void AddExplosionForce(Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius) {
        var dir = (body.transform.position - expPosition);
        float calc = 1 - (dir.magnitude / expRadius);
        if (calc <= 0) {
            calc = 0;
        }

        body.AddForce(dir.normalized * expForce * calc);
    }

    void OnTriggerEnter2D(Collider2D otherBox) {
        Debug.Log("triggered");
        Debug.Log(transform.position);
        Debug.Log(otherBox.gameObject.tag);
        if (otherBox.gameObject.tag.Equals("Obstacle"))
        {
            //Game over
            AddExplosionForce(otherBox.GetComponent<Rigidbody2D>(), 100, this.transform.position, 3f);
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            TriggerEndGame();
        }
    }

    /// <summary>
    /// check to see if this score is better than previous and store data locally
    /// </summary>
    private void CollectAndStoreData() {
        if (controller.bestDistance < score) {
            //new Best!
            controller.bestDistance = score;
        }
        GameCenterController.PersistScoreToGamecenterDatabase(score);
        GameCenterController.saveScoreToMyDatabase(score);
        controller.save();
    }

    private void ReportAchievementProgress() {

			GameCenterController.reportProgressForAchievement("grp.25Jumps.05acfb57f71b4a58bf29632aadbbaaf", score / 25);
            GameCenterController.reportProgressForAchievement("grp.50Jumps.0b812bd06d1f430a8d1467323a43b443", score / 50);
            GameCenterController.reportProgressForAchievement("grp.75Jumps.05acfb57f71b4a58bf29632aadbbaaf7", score / 75);
            GameCenterController.reportProgressForAchievement("grp.100Jumps.ce2f2d3b2eb144eb8dfa404a625a945e", score / 100);
            GameCenterController.reportProgressForAchievement("grp.150Jumps.05acfb57f71b4a58bf29632aadbbaaf7", score / 150);
            GameCenterController.reportProgressForAchievement("grp.200Jumps.05acfb57f71b4a58bf29632aadbbaaf7", score / 200);
            GameCenterController.reportProgressForAchievement("grp.300Jumps.05acfb57f71b4a58bf29632aadbbaaf7", score / 300);
            GameCenterController.reportProgressForAchievement("grp.500Jumps.05acfb57f71b4a58bf29632aadbbaaf7", score / 500);
    }

    /// <summary>
    /// Trigger the endgame.
    /// </summary>
    private void TriggerEndGame() {

        isGameOver = true;
        CollectAndStoreData();
        ReportAchievementProgress();
        ShowEndScreen();
        ShowInterstialIfNeeded();

        //display this score
        endScoreText.GetComponent<Text>().text = score.ToString();

        //display the best score
        endScoreBest.GetComponent<Text>().text = controller.bestDistance.ToString();

        //hide player
        gameObject.SetActive(false);

    }

    private void ShowInterstialIfNeeded() {
        Debug.Log(GameController.control.timesPlayedToday);
        if (GameController.control.timesPlayedToday % 3 == 0) {
            Debug.Log("Showing Interstitial");
            Debug.Log(GameController.control.timesPlayedToday);
            AdvertManager.adManager.ShowInterstitial();
        } else if (GameController.control.timesPlayedToday % 3 == 1) {
            AdvertManager.adManager.RequestInterstitial();
        }
    }

    public void IncrementScore() {
        score++;
        distanceText.text = "Score: " + score;
    }

    private void ShowEndScreen() {
        //hide in game overlay
        inGameGUI.SetActive(false);
        losePanelAnimator.Play("MoveOver");
    }

    private bool IsMoveValid() {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Waiting") && cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Waiting") && !isGameOver;
    }

    private void MoveCamera() {
        cameraAnimator.Play("moveCameraUp");
    }

    private void MoveBackground() {
        backgroundAnimator.Play("Move");
    }

    private void PlayMoveAnimation(string animationName) {
        Debug.Log(animationName);
        playerAnimator.Play(animationName);
    }

}
