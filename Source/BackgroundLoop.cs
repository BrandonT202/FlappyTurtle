using UnityEngine;
using System.Collections;

public class BackgroundLoop : MonoBehaviour {

	//Pipe Variables
	float numOfPanels = 6;
	float PipeMin = 0f;
	float PipeMax = 2.88f;
	float offset = 0.25f;

	void Start()
	{
		GameObject[] pipes = GameObject.FindGameObjectsWithTag ("Pipe");
		float prevY = 0.0f;

		foreach(GameObject pipe in pipes)
		{
			Vector3 pos = pipe.transform.position;

			pos.y = Random.Range(PipeMin, PipeMax);

			if(pos.y > prevY && pos.y < PipeMax)
			{
				pos.y -= offset; 
			}
			else if(pos.y < prevY && pos.y > PipeMin)
			{
				pos.y += offset;
			}

			prevY = pos.y;
			pipe.transform.position = pos; 
		}
	}

	void OnTriggerEnter2D( Collider2D col )
	{
		Debug.Log ("Triggered: " + col.name); 

		float widthOfObject = ((BoxCollider2D)col).size.x;

		Vector3 pos = col.transform.position; 

		pos.x += widthOfObject * numOfPanels; 

		if(col.tag == "Pipe")
		{
			pos.y = Random.Range(PipeMin, PipeMax);
		}

		col.transform.position = pos; 
	}
}
