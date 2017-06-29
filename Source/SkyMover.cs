using UnityEngine;
using System.Collections;

public class SkyMover : MonoBehaviour {

	Rigidbody2D player;
	float fractionOfPlayerSpeed = 0.9f;
	
	void Start () 
	{
		GameObject player_GO = GameObject.FindGameObjectWithTag ("Player"); 
		
		if(player_GO == null)
		{
			Debug.LogError("Couldn't find the GameObject with tag 'Player'");
			return;
		}
		
		player = player_GO.GetComponent<Rigidbody2D>(); 
	} 

	// Update is called once per frame
	void FixedUpdate ()
	{
		float vel = player.velocity.x * fractionOfPlayerSpeed;  
		transform.position += Vector3.right * vel * Time.deltaTime;
	}
}
