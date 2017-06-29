using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static public int score = 0; 
	static public int highScore = 0; 
	static Score instanceScore; 

	RigidBodyJumpScript theBird;

	static public void AddPoint()
	{
		if(instanceScore.theBird.dead)
		{
			return;
		}

		score++;

		if(score > highScore)
		{
			highScore = score; 
		}
	}
	
	void Start()
	{
		instanceScore = this;
		GameObject player_GO = GameObject.FindGameObjectWithTag ("Player");
		if(player_GO == null)
		{
			Debug.LogError("Could not find a GameObject with tag 'Player'.");
		}
		theBird = player_GO.GetComponent<RigidBodyJumpScript> (); 
		score = 0;
		highScore = PlayerPrefs.GetInt ("HighScore", 0);
	}

	void Update()
	{
		GetComponent<GUIText>().text = score.ToString ();
	}

	void OnDestroy()
	{
		instanceScore = null;
		PlayerPrefs.SetInt ("HighScore", highScore); 
	}
}


