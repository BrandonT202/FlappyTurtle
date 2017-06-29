using UnityEngine;
using System.Collections;

public class CamTrackPlayer : MonoBehaviour {

	Transform player;

	float offset; 

	// Use this for initialization
	void Start () 
	{
		GameObject player_GO = GameObject.FindGameObjectWithTag ("Player"); 

		if(player_GO == null)
		{
			Debug.LogError("Couldn't find the GameObject with tag 'Player'");
		}

		player = player_GO.transform; 

		offset = transform.position.x - player.position.x; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
		{
			Vector3 pos = transform.position;
			pos.x = player.position.x + offset; 
			transform.position = pos;  
		}
	}
}
