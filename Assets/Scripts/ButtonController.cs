using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    public void loadLevel(string name) {
		gameObject.GetComponent<Button>().enabled = false;
        LevelManager.instance.StartLoading(name);
    }


	public void showLeaderboards()
	{
        GameCenterController.showLeaderboard();
	}

    public void ShowAchievements() {
        GameCenterController.ShowAchievements();
    }
}
