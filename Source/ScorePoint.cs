using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {

	private bool inScoreBox = false; 
	private AudioSource audio;

	void Start()
	{
		audio = GameObject.Find ("Audio").GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		if(transform.tag == "ScoreBox")
		{
			if(col.tag == "Player")
			{
				inScoreBox = true; 
				Debug.Log("Detected Player"); 
			}
		}
	}

	void OnTriggerExit2D( Collider2D col ) 
	{
		if(col.tag == "Player")
		{
			if(inScoreBox)
			{
				SpriteScore.AddPoint ();
				inScoreBox = false;
				audio.Play (); 
			}
		}
	}
}
