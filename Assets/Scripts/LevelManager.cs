using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public LevelManager instance;
	public GameObject MenuScreen;

	public float autoLoadNextLevelAfter;

	/// <summary>
	/// This is the main method that instantiates the Level Manager class
	/// Only a single instance can exist. This will ensure that there are no duplicates
	/// when reloading a scene.
	/// </summary>
	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject); 
		}
	}

	/// <summary>
	/// Load the level with the given name
	/// </summary>
	/// <param name="name">Name.</param>
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	/// <summary>
	/// Loads the main game.
	/// </summary>
	public void LoadMainGame()
	{
		SceneManager.LoadSceneAsync("01Level");
		MenuScreen.active = false;
	}

	/// <summary>
	/// Quits the request.
	/// </summary>
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void loadNextLevel(){
		Application.LoadLevel (Application.loadedLevel + 1);
	}

}
