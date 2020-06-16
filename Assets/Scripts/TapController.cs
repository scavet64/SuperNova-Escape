using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapController : MonoBehaviour {

    private Animator TapAnimator;
    private Image image;

    // Use this for initialization
    void Start () {
        TapAnimator = this.GetComponent<Animator>();
        image = GetComponent<Image>();
        float firstRock = Spawner.CurrentSpawnedRocks[0].transform.position.x;
        if(firstRock < 0) {
            //rock is on left
            this.transform.position += new Vector3(270, -275);
        } else {
            //rock is on right
            this.transform.position += new Vector3(-270, -275);
        }

        image.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void fadeAway() {
        TapAnimator.Play("FadeAway");
    }
}
