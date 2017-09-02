using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GravityBody))]
public class firstPersonController : MonoBehaviour {

	// public vars
	public float mouseSensitivityX = 1;
	public float mouseSensitivityY = 1;
	public float walkSpeed = 10;
	public float jumpForce = 500;
	public LayerMask GroundedMask;
	public bool isGrounded;

	// System vars
	Vector3 moveAmount;
	Vector3 smoothMoveVelocity;
	float verticalLookRotation;
	Transform cameraTransform;
	Rigidbody rigidbody;


	void Awake() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody> ();
	}

	void Update() {

		// Camera rotation:
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

		// Capsule movement:
		Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
		Vector3 targetMoveAmount = moveDir * walkSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount,targetMoveAmount,ref smoothMoveVelocity,.15f);

		// Jump
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (isGrounded) {
				rigidbody.AddForce(transform.up * jumpForce * 9.8f);
			}
		}

		// Grounded check
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1 + .1f, GroundedMask)) {
			isGrounded = true;
		}
		else {
			isGrounded = false;
		}
	}

	void FixedUpdate() {

		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);
		Debug.Log ("Player is isGrounded: " + isGrounded);
	}

	void OnTriggerEnter(Collider col)
	{
		StayOnSphere (col);
	}

	void OnTriggerStay(Collider col)
	{
		StayOnSphere (col);
	}

	void StayOnSphere(Collider col)
	{
		isGrounded = true;
	}
}
