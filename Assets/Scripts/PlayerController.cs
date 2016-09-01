using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public static int leftButtonSender = 1;
    public static int rightButtonSender = 2;

    public GameObject cameraObject;

    private Animator cameraAnimator;
    private Animator playerAnimator;
    private bool isShipOnLeft;


	// Use this for initialization
	void Start () {

        Time.timeScale = 1;
        isShipOnLeft = false;
        playerAnimator = GetComponent<Animator>();
        cameraAnimator = cameraObject.GetComponent<Animator>();
        //animator.enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("TEST");
    }

    public void moveShip(int i) {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Waiting")) {

            if (i == leftButtonSender) {
                if (isShipOnLeft) {
                    //LeftToLeft
                    playMoveAnimation("RightToRight");
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
                    playMoveAnimation("RightToRight");
                }
            }

            moveCamera();
        }
    }

    private void moveCamera() {
        cameraAnimator.Play("moveCameraUp");
    }

    private void playMoveAnimation(string animationName) {
        Debug.Log(animationName);
        //Debug.Log(animator);
        //playerAnimator.enabled = true;
        playerAnimator.Play(animationName);

        //move camera up or world down

    }

}
