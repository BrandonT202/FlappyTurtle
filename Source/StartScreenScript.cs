using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

 	static bool appeared = false; 

	// Use this for initialization
	void Start () 
	{
		if(!appeared)
		{
			GetComponent<SpriteRenderer>().enabled = true; 
			Time.timeScale = 0; 
		}

		appeared = true; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ( Time.timeScale == 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) )
		{
			Time.timeScale = 1;
			GetComponent<SpriteRenderer>().enabled = false; 
		}
	}
}
