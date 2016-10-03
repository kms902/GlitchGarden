using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text myText;
	
	void Start()
	{
		// To access the Text component, using UnityEngine.UI (line 2)
		myText = GetComponent<Text>();
		Reset ();
	}
	
	public void Score(int points)
	{
		Debug.Log ("Scored Points");
		score += points;
		myText.text = score.ToString();
	}
	
	public static void Reset()
	{
		score = 0;
		//myText.text = score.ToString();
	}
}
