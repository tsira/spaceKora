using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityAttractor : MonoBehaviour {

	public float gravity = -9.8f;
	private static List<GravityBody> players = new List<GravityBody>();

	void Start() {

	}

	public static void AddObject (GravityBody newObject)
	{
		players.Add (newObject);
	}

	public static void RemoveObject (GravityBody existingObject)
	{
		players.Remove (existingObject);
	}

	void OnApplicationQuit()
	{
//		Debug.Log("Number of Players in System: "+ players.Count);
//		foreach (GravityBody player in players) {
//			Debug.Log ("Player List: " + player.name);
//		}
		players = new List<GravityBody>();
	}

//	void LateUpdate()
//	{
//		foreach (GravityBody player in players) {
//
//			float distance = Vector3.Distance(player.transform.position, player.GetComponent<Rigidbody>().position);
//			Debug.Log("Distance between player: "+ player.name + " and Player is = " + distance); 
//			Vector3 gravityUp = (player.GetComponent<Rigidbody>().position - transform.position).normalized;
//			Vector3 localUp = player.GetComponent<Rigidbody>().transform.up;
//			
//			player.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
//			player.GetComponent<Rigidbody>().rotation = Quaternion.FromToRotation(localUp,gravityUp) * player.GetComponent<Rigidbody>().rotation;
//		}
//	
//	}

	public void Attract(Rigidbody body) {
		foreach (GravityBody player in players) {
			Vector3 gravityUp = (body.position - transform.position).normalized;
			Vector3 localUp = body.transform.up;
			body.AddForce (gravityUp * gravity);
			//body.rotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;

		}
	}

	public void Attract(Rigidbody body, float gravForce) {
		foreach (GravityBody player in players) {
			Vector3 gravityUp = (body.position - transform.position).normalized;
			Vector3 localUp = body.transform.up;
			body.AddForce (gravityUp * -gravForce);
		}
	}

	public void Capture(Rigidbody body) {
		Vector3 gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;
		body.rotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;
	}
}