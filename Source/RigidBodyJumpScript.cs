using UnityEngine;
using System.Collections;

public class RigidBodyJumpScript : MonoBehaviour {
	
	public float flapSpeed = 375f;
	public float forwardSpeed = 1.25f;
	float torque = 4.0f;
	
	Animator animator;

	bool hasFlapped = false;
	public bool dead = false; 

	float deathCoolDownTime = 0.5f;
	
	// Use this for initialization
	void Start () 
	{
		animator = transform.GetComponentInChildren<Animator> ();
		
		if(animator == null)
		{
			Debug.LogError("Didn't find Animator"); 
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(dead)
		{
			deathCoolDownTime -= Time.deltaTime;

			if(deathCoolDownTime <= 0)
			{
				if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
				{
					Application.LoadLevel( Application.loadedLevel ); 
				}
			}
		}
		else
		{
			if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
			{
				hasFlapped = true; 
			}
		}
	}

	void FixedUpdate ()
	{
		if(dead)
		{
			return;
		}

		GetComponent<Rigidbody2D>().AddForce ( Vector2.right * forwardSpeed ); 
		
		if(hasFlapped)
		{
			GetComponent<Rigidbody2D>().AddForce( Vector2.up * flapSpeed );
			animator.SetTrigger("hasFlapped"); 
			GetComponent<AudioSource>().Play();
			hasFlapped = false; 
		}

		if(GetComponent<Rigidbody2D>().velocity.y > 0)
		{
			transform.rotation = Quaternion.Euler(0, 0, 20f);
		}
		else
		{
			float angle = Mathf.Lerp(20, -90, (-GetComponent<Rigidbody2D>().velocity.y / torque));
			transform.rotation = Quaternion.Euler(0,0, angle); 
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		animator.SetTrigger ("Death");
		dead = true; 
	}
}
