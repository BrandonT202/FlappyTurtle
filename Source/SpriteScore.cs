using UnityEngine;
using System.Collections;

public class SpriteScore : MonoBehaviour {

	static public int score = 0; 
	static public int highScore = 0; 
	static SpriteScore instanceScore; 
	
	RigidBodyJumpScript theBird;

	private string sortingLayer = "Score"; 
	private const float SPRITE_WIDTH = 0.3f;
	private const string SPRITE_TAG = "Score_Tag"; 
	
	static public void AddPoint()
	{
		if(instanceScore.theBird.dead)
		{
			return;
		}
		
		score++;
		Debug.Log (score);
		
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

	void OnDestroy()
	{
		instanceScore = null;
		PlayerPrefs.SetInt ("HighScore", highScore); 
	}

	void FixedUpdate()
	{
		UpdateScoreDisplay (); 
	}

	/*
		* 1 Find score string
			* 2 Find sub-string (units) and instantiate a variable with the data
			* 2a Find sub-string (tens) and instantiate a variable with the data
			* 2b Find sub-string (hundreds) and instantiate a variable with the data
			* 3 Loop through digits to disable any irrelevent sprites (if equals tens or hundreds == '0' then disable)
	*/
	
	void ResetDigits()
	{
		GameObject[] oldScoreDigits = GameObject.FindGameObjectsWithTag (SPRITE_TAG); 
		foreach( GameObject digit in oldScoreDigits )
		{
			Destroy(digit); 
		}
	}

	void DefaultScore()
	{
		score = 0;
	}

	void UpdateScoreDisplay () 
	{
		string scoreStr = score.ToString ();
		SpriteRenderer sprite; 

		//remove previous score
		ResetDigits (); 

		for(int i = 0; i < scoreStr.Length; i++)
		{
			string objectStr = scoreStr.Substring(i,1); 
			GameObject theDigit = GameObject.Find(objectStr);
			if(theDigit == null)
			{
				Debug.LogError("Can't Instantiate Object: " + scoreStr.Substring(i,1));
			}
			GameObject digit = Instantiate(theDigit) as GameObject;
			sprite = digit.GetComponent<SpriteRenderer>(); 

			digit.tag = SPRITE_TAG; 
			digit.transform.parent = this.transform; 
			digit.transform.localPosition = new Vector3(i * SPRITE_WIDTH, 0, 10);
			sprite.sortingOrder = 0; 
			sprite.sortingLayerName = sortingLayer;
		}

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2f, Screen.height * 0.85f, 1));
     	pos.x -= scoreStr.Length * SPRITE_WIDTH;
     	transform.position = pos;
	}
}
