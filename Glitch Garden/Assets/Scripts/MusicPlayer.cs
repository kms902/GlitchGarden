using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Awake (){
		if (instance != null && instance != this){
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play ();
		}
	}
	
	void OnLevelWasLoaded(int level)
	{
		Debug.Log ("MusicPlayer: loaded level " + level);
		music.Stop();
		
		if(level == 0){
			music.clip = startClip;
		}
		else if(level == 1){
			music.clip = gameClip;
		}
		else if(level == 2){
			music.clip = endClip;
		}
		music.loop = true;
		music.Play ();
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
