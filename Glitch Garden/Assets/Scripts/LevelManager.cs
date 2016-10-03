using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextlevelAfter;
	
	void Start()
	{
		Invoke ("LoadNextLevel", autoLoadNextlevelAfter);
	}

	public void LoadLevel(string name){
		Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel (name);
	}
	
	public void QuitRequest(){
		Debug.Log ("Quit requested.");
		// Application.Quit() works great for a PC/console build, but not a debug 
		// or web version... dont use in mobile, quit buttons get rejected
		Application.Quit ();
	}
	
	public void LoadNextLevel(){
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
}
