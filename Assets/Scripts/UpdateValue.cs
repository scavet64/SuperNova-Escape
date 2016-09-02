using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateValue : MonoBehaviour {

    private Text distanceText;
    private int value;

	// Use this for initialization
	void Start () {
        distanceText = GetComponent<Text>();
        value = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void incrementScore() {
        value++;
        distanceText.text = "Score: " + value;

    }
}
