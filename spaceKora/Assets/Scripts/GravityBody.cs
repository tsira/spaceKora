using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

	private static List<GravityAttractor> planets = new List<GravityAttractor>();
	public float gravForce;

	void Awake () {
		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("planet")) {
			planets.Add(planet.GetComponent<GravityAttractor>());
		}
		//planets.Add(GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>());
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().freezeRotation = true;
	}

	void Start() {
		GravityAttractor.AddObject (this); 
	}

	void OnDestroy(){
		GravityAttractor.RemoveObject (this);
	}

	void FixedUpdate () {
		foreach (GravityAttractor planet in planets) {
			//Debug.Log("Distance between player and: "+planet.name+ " is: "+Vector3.Distance(planet.transform.position, GetComponent<Rigidbody>().position));
			//Debug.Log("Mass of Planet: "+plane t.name+ " is: "+planet.GetComponent<Rigidbody> ().mass);
			Debug.Log ("Gravitational Force of: " + planet.name + " on the player is: " + (0.06 * planet.GetComponent<Rigidbody> ().mass) / Mathf.Pow(0.00001f + (Vector3.Distance (planet.transform.position, GetComponent<Rigidbody> ().position)),2));
			//Debug.Log ("Planet name is: " + planet.name);
			gravForce = (float)(0.06 * planet.GetComponent<Rigidbody> ().mass) / Mathf.Pow(0.00001f + (Vector3.Distance (planet.transform.position, GetComponent<Rigidbody> ().position)),2);
			planet.Attract (GetComponent<Rigidbody> (), gravForce);
		}
		planets.Sort (delegate(GravityAttractor x, GravityAttractor y) {
			return (int)Vector3.Distance (x.transform.position, GetComponent<Rigidbody> ().position).CompareTo(Vector3.Distance (y.transform.position, GetComponent<Rigidbody> ().position));
		});
		planets[0].Capture (GetComponent<Rigidbody> ());
	}
		
}