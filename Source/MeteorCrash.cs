using UnityEngine;
using System.Collections;

public class MeteorCrash : MonoBehaviour {

	Vector3 Opos;
	Vector3 Orot; 

	public Transform[] pointsOfExplosion;
	private Animator anim; 

	public float smoothing = 0.05f;
	public int rand; 

	// Use this for initialization
	void Start () 
	{
		rand = Random.Range (0, 3); 
		anim = transform.GetComponentInChildren<Animator> (); 
		SetOriginalPosition ();  
		if(AtOrginalPosition())
		{
			anim.SetTrigger("Falling");
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		transform.position = Vector3.Lerp (transform.position, pointsOfExplosion[rand].position, Time.deltaTime * smoothing);

		Vector3 near = pointsOfExplosion[rand].position - transform.position; 
		if((near.x <= 5 && near.y <= 5) || (near.x <= -5 && near.y <= -5))
		{
			float ratio = (pointsOfExplosion[rand].position.y/transform.position.y); 
			smoothing += (Time.deltaTime * ratio * ratio / smoothing); 
		}

		if(AtExplosionPosition())
		{
			Debug.Log("Nearing explosion");
			anim.ResetTrigger("Falling"); 
			anim.SetTrigger("Explosion"); 

			transform.position = pointsOfExplosion[rand].position; 
			transform.eulerAngles = Vector3.zero; 

			StartCoroutine(ExplosionTime()); 
		}
	}

	void SetOriginalPosition()
	{
		Opos = transform.position; 

		if(transform.position.x < 0)
		{
			Orot = new Vector3(0, 0, Vector3.Angle (transform.position, pointsOfExplosion[rand].position));
		}
		else
		{
			Orot = new Vector3(0, 0, -Vector3.Angle (transform.position, pointsOfExplosion[rand].position));
		}

		Debug.Log(Orot);
		transform.eulerAngles = Orot;
	}

	bool AtOrginalPosition()
	{
		if((transform.position.x == Opos.x) && (transform.position.y == Opos.y))
		{
			return true; 
		}
		else
			return false;
	}

	bool AtExplosionPosition()
	{
		float approxY = pointsOfExplosion[rand].position.y + 0.25f;

		if(transform.position.y < approxY)
		{
			return true; 
		}
		else
			return false;
	}

	IEnumerator ExplosionTime()
	{
		yield return new WaitForSeconds(0.75f); 
		Destroy (gameObject);
	}
}
