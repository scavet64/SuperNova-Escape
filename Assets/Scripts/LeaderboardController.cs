using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour {

	public GameObject MenuPanel;

	private Animator leaderboardAnimator;

	// Use this for initialization
	void Start () {
		leaderboardAnimator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowLeaderboardPanel()
	{
		MenuPanel.SetActive(false);
		leaderboardAnimator.Play("MoveOver");
	}

	public void ShowMenuPanel()
	{

	}
}
