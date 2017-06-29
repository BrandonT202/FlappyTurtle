using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {
	
	// Update is called once per frame
	void OnTriggerEnter2D ( Collider2D col) 
	{
		if(col.tag == "Player")
		{
			//if player is off the screen 
			if(col.transform.position.y < transform.position.y) 
			{
				RigidBodyJumpScript script = col.GetComponent<RigidBodyJumpScript>();
				script.dead = true;
			}
		}
	}
}
