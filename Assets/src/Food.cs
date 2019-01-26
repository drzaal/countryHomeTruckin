using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField] float force;
	private Rigidbody rb;
	private SphereCollider bc;

	private bool isContained;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<SphereCollider>();
		isContained = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isContained && bc.isTrigger && rb.velocity.y < 0) {
			bc.isTrigger = false;
		}
	}

	public void Pickup() {
		Physics.IgnoreCollision(transform.parent.parent.GetComponent<BoxCollider>(), bc);
		isContained = true;
		rb.isKinematic = false;
		rb.AddForce(0, force, 0, ForceMode.Impulse);
	}
}
