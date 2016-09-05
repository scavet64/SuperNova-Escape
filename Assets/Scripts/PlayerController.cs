using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        //distanceText = GetComponent<Text>();
        score = 0;
        isGameOver = false;
        //animator.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("TEST");
    }

    public void moveShip(int i) {
        if (isMoveValid()) {
            
            //determine which animation to play
            if (i == leftButtonSender) {
                if (isShipOnLeft) {
                    //LeftToLeft
                    playMoveAnimation("UpWiggleRight");
                } else {
                    //rightToLeft
                    playMoveAnimation("RightToLeft");
                    isShipOnLeft = true;
                }
            } else {
                //rightbutton is sender
                if (isShipOnLeft) {
                    //leftToRight
                    playMoveAnimation("LeftToRight");
                    isShipOnLeft = false;
                } else {
                    //rightToRight
                    playMoveAnimation("UpWiggleLeft");
                }
            }

            playMovementAudio();
            rockSpawner.spawnRocksIfNeeded();
            incrementScore();
            moveCamera();
            moveBackground();
        }
    }

    private void playMovementAudio() {
        audioSource.clip = movementSounds[Random.Range(0, movementSounds.Length - 1)];
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

        AddExplosionForce(otherBox.GetComponent<Rigidbody2D>(), 100, this.transform.position, 3f);

        //trigger endgame
        Instantiate(explosionPrefab,this.transform.position, Quaternion.identity);

        triggerEndGame();

    }

    //void OnCollisionEnter2D(Collision2D otherBox) {
    //    Debug.Log("BOOM");
    //    Debug.Log(transform.position);

    //    //otherBox.rigidbody.AddExplosionForce(100f, this.transform.position, 4f);

    //    AddExplosionForce(otherBox.rigidbody, 100, this.transform.position, 3f);

    //    //trigger endgame
    //    Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
    //    triggerEndGame();
    //}

    private void triggerEndGame() {

        showEndScreen();
        isGameOver = true;

        endScoreText.GetComponent<Text>().text = score.ToString();

        //TODO: NEED TO IMPLEMENT BEST
        endScoreBest.GetComponent<Text>().text = "Error";

        //hide player
        gameObject.SetActive(false);

    }

    public void incrementScore() {
        score++;
        distanceText.text = "Score: " + score;

    }

    private void showEndScreen() {
        //hide in game overlay
        inGameGUI.SetActive(false);
        losePanelAnimator.Play("MoveOver");
    }

    private bool isMoveValid() {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Waiting") && cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("Waiting") && !isGameOver;
    }

    private void moveCamera() {
        cameraAnimator.Play("moveCameraUp");
    }

    private void moveBackground() {
        backgroundAnimator.Play("Move");
    }

    private void playMoveAnimation(string animationName) {
        Debug.Log(animationName);
        //Debug.Log(animator);
        //playerAnimator.enabled = true;
        playerAnimator.Play(animationName);

        //move camera up or world down

    }

}
