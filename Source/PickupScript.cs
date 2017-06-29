using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {

	private SpriteRenderer sprite;
	private CircleCollider2D spriteCollider;
	private RigidBodyJumpScript playerScript;
	private Transform player = null;

	private GameObject[] Meteors = new GameObject[3];
	private GameObject theMeteor; 
	private GameObject meteorSpawn; 

	bool ActivePickup = false;

	//duration of the pickup effect
	float pickupDuration = 5f;

	//pickup tag
	const string SCALETAG = "ScalePickup"; 
	const string SPEEDTAG = "SpeedPickup"; 
	const string GRAVITYTAG = "GravityPickup";
	const string METEORTAG = "MeteorPickup"; 

	//scale variables
	Vector3 originalSize = new Vector3(1f,1f,1f);
	Vector3 newSize = new Vector3 (1.5f, 1.5f, 1f);
	float smoothing = 2f;

	//speed and gravity variables
	float originalSpeed;
	float newSpeed;
	float normalGravity = 1.25f;
	float newGravity = 0.75f;
	float originalFlapSpeed; 
	float newFlapSpeed; 
	
	void Start()
	{
		sprite = transform.GetComponentInChildren<SpriteRenderer> (); 
		spriteCollider = transform.GetComponent<CircleCollider2D> (); 

		player = GameObject.FindWithTag ("Player").transform;
		playerScript = player.GetComponent<RigidBodyJumpScript> ();

		meteorSpawn = GameObject.Find("MeteorSpawn");
		theMeteor = GameObject.Find ("Meteor");

		originalSpeed = playerScript.forwardSpeed; 
		originalFlapSpeed = playerScript.flapSpeed; 
		newSpeed = playerScript.forwardSpeed * 1.25f;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("Entered Trigger");
		if(	col.tag == "Player" )
		{
			ActivePickup = true; 
			sprite.enabled = false;
			spriteCollider.enabled = false;
			StartCoroutine (Timer ());
		}
	}

	void Update()
	{
		if(ActivePickup)
		{ 
			if(transform.tag == SCALETAG)
			{
				ScaleUpPlayer(); 
			}
			if(transform.tag == SPEEDTAG)
			{
				SpeedUpPlayer(); 
			}
			if(transform.tag == GRAVITYTAG)
			{
				ChangeGravity(); 
			}
			if(transform.tag == METEORTAG)
			{
				StartCoroutine( SpawnMeteor() ); 
			}

			if(!ActivePickup)
			{
				GetComponent<AudioSource>().Play(); 
			}
		}
	}

	IEnumerator Timer()
	{	
		yield return new WaitForSeconds(pickupDuration);
		Reset (); 
		Destroy (gameObject); 
	}

	void ScaleUpPlayer()
	{
		player.localScale = Vector3.Lerp (player.localScale, newSize, Time.deltaTime * smoothing);
		if(player.localScale.x > 1.45f && player.localScale.y > 1.45f)
		{
			player.localScale = newSize;
			ActivePickup = false;
		}
		Debug.Log (player.localScale.x + " " + player.localScale.y);
	}

	void SpeedUpPlayer ()
	{
		playerScript.forwardSpeed = newSpeed;
		player.transform.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
		ActivePickup = false; 
	}

	void ChangeGravity()
	{
		player.transform.GetComponent<Rigidbody2D>().gravityScale = newGravity;
		playerScript.flapSpeed *= 0.75f;
		ActivePickup = false; 
	}

	IEnumerator SpawnMeteor()
	{ 
		ActivePickup = false; 
		for(int meteor = 0; meteor < 3; meteor++)
		{	
			Meteors[meteor] = Instantiate (theMeteor, meteorSpawn.transform.position, Quaternion.identity) as GameObject;
			Meteors[meteor].GetComponent<MeteorCrash> ().enabled = true;
			Meteors[meteor].transform.parent = Camera.main.transform;
			yield return new WaitForSeconds (2); 
		}
	}

	void Reset()
	{
		switch(transform.tag)
		{
			case SCALETAG:
				player.localScale = originalSize;
				break;
			case SPEEDTAG:
				playerScript.forwardSpeed = originalSpeed; 
				player.transform.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
				break;
			case GRAVITYTAG:
				player.transform.GetComponent<Rigidbody2D>().gravityScale = normalGravity;
				playerScript.flapSpeed = originalFlapSpeed;
				break;
			default:
				break; 
		}
		Debug.Log ("Reset!"); 
	}
}
