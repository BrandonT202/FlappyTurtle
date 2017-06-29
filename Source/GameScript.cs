using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScript: MonoBehaviour
{
	public GameObject ScalePickup; 
	public GameObject SpeedPickup; 
	public GameObject GravityPickup; 
	public GameObject MeteorPickup; 
	bool spawnPickup = true;
	int spawnDelay = 25; 

	Transform player; 

	void Start()
	{
		player = GameObject.FindWithTag ("Player").transform;
	}
	
	void Update()
	{
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }

		if(spawnPickup)
		{
			Debug.Log("Pickup!"); 
			spawnPickup = false; 
			StartCoroutine(SpawnPickup()); 
		}
	}

	IEnumerator SpawnPickup()
	{
		yield return new WaitForSeconds(spawnDelay); 

		GameObject pickup = InstantiatePickup ();
		Debug.Log ("spawn");
	
		if (pickup == null)
			Debug.LogError ("Pickup Invalid");

		string ParentName = RandomParent (); 
		GameObject newParent = GameObject.Find (ParentName);
		if(newParent.transform.position.x < player.transform.position.x)
		{
			ParentName = RandomParent(); 
			newParent = GameObject.Find (ParentName);
		}

		if(newParent != null)
		{
			Debug.Log (ParentName);
			pickup.transform.parent = newParent.transform;
			pickup.transform.localPosition = new Vector3 (0f, 4.75f, 0f);
			spawnPickup = true; 
		}
		else
			Debug.Log("Invalid Parent");
	}

	GameObject InstantiatePickup()
	{
		int choice = Random.Range (0, 4);
		GameObject pickup = null; 

		switch(choice)
		{
		case 0:
			pickup = Instantiate (ScalePickup, Vector3.zero, Quaternion.identity) as GameObject;
			break;
		case 1:
			pickup = Instantiate (SpeedPickup, Vector3.zero, Quaternion.identity) as GameObject;
			break;
		case 2:
			pickup = Instantiate (GravityPickup, Vector3.zero, Quaternion.identity) as GameObject;
			break;
		case 3:
			pickup = Instantiate(MeteorPickup, Vector3.zero, Quaternion.identity) as GameObject;
			break; 
		default:
			break; 
		}

		return pickup; 
	} 

	string RandomParent()
	{
		int RandomNumber = Random.Range (0, 5);
		string ParentName = "Pipes" + RandomNumber;
		return ParentName; 
	}
}
