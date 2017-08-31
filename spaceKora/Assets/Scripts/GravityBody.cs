using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {

	GravityAttractor planet;
	Rigidbody rigidbody;

	void Awake () {
		planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>();
		//GameObject player = GameObject.FindGameObjectWithTag("Player").GetComponent<GravityAttractor>();
		rigidbody = GetComponent<Rigidbody> ();
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		planet.Attract(rigidbody);
	}
}