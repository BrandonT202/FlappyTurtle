using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

	public Vector3 velocity = Vector3.zero;
	public Vector3 gravity; 
	public Vector3 flapVelocity;
	public float maxSpeed = 5.0f;
	public float forwardVelocity = 2f; 
	public float torque = 2f; 
	float angle = 0f;


	Animator animator; 

	bool hasFlapped = false;
	bool dead = false; 

	void Start()
	{
		animator = transform.GetComponentInChildren<Animator> (); 

		if(animator == null)
		{
			Debug.LogError("Didn't find Animator"); 
		}
	}

	//Do Physics engine updates here
	void FixedUpdate ()
	{
		if(dead)
		{
			return;
		}

		velocity -= gravity * Time.deltaTime; 
		velocity.x = forwardVelocity; 

		if(hasFlapped)
		{
			animator.SetTrigger("hasFlapped"); 
			if(velocity.y < 0)
			{
				velocity.y = 0f;
				velocity.x = 0f;
			}
			velocity += flapVelocity; 
			hasFlapped = false;
		}
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed); 
		transform.position += velocity * Time.deltaTime;

		if(velocity.y > 0)
		{
			transform.rotation = Quaternion.Euler(0, 0, 10f);
		}
		else
		{
			angle = Mathf.Lerp(10, -90, -velocity.y / torque);
			transform.rotation = Quaternion.Euler(0,0, angle); 
		}
	}
	
	// DO Graphics and Input updates here
	void Update () 
	{
		if(Input.GetButtonDown("Fire1") || Input.GetKey(KeyCode.Space))
		{
			hasFlapped = true; 
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		animator.SetTrigger ("Death"); 
		dead = true;
	}
}
