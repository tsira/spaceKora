using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class playerGravityBody : MonoBehaviour {

	public playerGravityAttractor attractor;
	private Transform myTransform;

	void Start () 
	{
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

		myTransform = transform;
	}

	void FixedUpdate () 
	{
		if (attractor)
		{
			attractor.Attract(myTransform);
		}
	}
}