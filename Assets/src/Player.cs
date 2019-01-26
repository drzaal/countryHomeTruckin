using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float maxSpeed;
	[SerializeField] float accelerateSpeed;
	[SerializeField] float reverseSpeed;
	[SerializeField] float turnSpeed;

	[SerializeField] float accelerationTime;
	[SerializeField] float reverseTime;

	[SerializeField] AnimationCurve accelerationCurve;
	[SerializeField] AnimationCurve reverseCurve;

	[SerializeField] Transform bed;

	private Rigidbody rb;
	private Vector3 velocity;
	private float isAccelerating;
	private float isReversing;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		velocity = new Vector3(0, 0, 0);
		isAccelerating = 0;
		isReversing = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float xmove = Input.GetAxisRaw("Horizontal");
		float zmove = Input.GetAxisRaw("Vertical");

		// handle forward and backward (z axis) movement
		if (zmove != 0) {
			// print(rb.velocity);
			if (zmove > 0) {
				// Accelerate(zmove);
				rb.velocity += transform.forward * accelerateSpeed;
				// transform.position += transform.forward * accelerateSpeed;
				// velocity = new Vector3(velocity.x, velocity.y, zmove * accelerateSpeed * Time.deltaTime);
				// transform.position = new Vector3(transform.position.x, transform.position.y, zmove * accelerateSpeed * Time.deltaTime);
			} else if (zmove < 0) {
				// Reverse();
				rb.velocity -= transform.forward * reverseSpeed;
				// transform.position -= transform.forward * reverseSpeed;
				// velocity = new Vector3(velocity.x, velocity.y, zmove * reverseSpeed * Time.deltaTime);
				// transform.position = new Vector3(transform.position.x, transform.position.y, zmove * reverseSpeed * Time.deltaTime);
			}
		} else {
			// print(rb.velocity);
			rb.velocity = rb.velocity * 0.975f;
		}

		// handle rotation (y axis)
		if (Mathf.Abs(rb.velocity.x) > 0.001f && Mathf.Abs(rb.velocity.z) > 0.001f && xmove != 0) {
			transform.Rotate(0, xmove * turnSpeed * Time.deltaTime, 0);
		}

		/* else {
			// Decelerate();
			// velocity = new Vector3(velocity.x, velocity.y, zmove * Time.deltaTime);
			// transform.position -= transform.forward * reverseSpeed;
			// transform.position = new Vector3(transform.position.x, transform.position.y, zmove * decelerateSpeed * Time.deltaTime);
		} */

		// velocity = new Vector3(velocity.x, velocity.y, Mathf.Max(0, velocity.z));
		// transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y, transform.position.z + velocity.z);
	}

	void LateUpdate() {
		transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
	}

	void Accelerate() {
		
	}

	void Reverse() {

	}

	void Decelerate() {

	}

	void OnTriggerEnter(Collider other) {
		Transform item = other.transform;
		if (other.transform.CompareTag("Food")) {
			item.GetComponent<Food>().Pickup();
		}

<<<<<<< HEAD
		if (other.transform.CompareTag("HomeZone") && GameManager.instance != null)
		{
			GameManager.instance.winLevel();
		}
=======
		/* if (other.transform.CompareTag("HomeZone"))
		{
			GameManager.instance?.winLevel();
		} */
>>>>>>> 80c8bf35fb8b95f8362bd54c3a2f6875066ca351
    }

	void Pickup(Transform item) {
		
	}
}
