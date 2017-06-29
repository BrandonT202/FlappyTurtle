using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float MovementSpeed = 1.2f; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += new Vector3 (MovementSpeed, 0, 0) * Time.deltaTime;
	}
}
