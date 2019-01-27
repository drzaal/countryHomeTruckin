using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField] float force;
	[SerializeField] float fallSpeed;

	public enum FoodType { PIG, PUMPKIN, CORN, COW, TURKEY, CABBAGE, POTATO }

	[SerializeField] public FoodType foodType;

	private Rigidbody rb;
	private CapsuleCollider cc;
	private Vector3 ogScale;
	private bool isContained;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CapsuleCollider>();
		ogScale = transform.localScale;
		isContained = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pickup() {
		if (!isContained) {
			transform.localScale = new Vector3(.5f, .5f, .5f); // make smaller
			gameObject.layer = 9; // change layer for collisions
			transform.position = GameManager.instance.bed.position; // put in cage
			isContained = true;
			cc.isTrigger = false;
			rb.isKinematic = false;
		}
	}

	public void Throw() {
		gameObject.layer = 0;
		isContained = false;
		rb.AddForce(new Vector3(Random.Range(-15, 15), Random.Range(5, 10), Random.Range(-15, 15)), ForceMode.Impulse);
	}

	void OnCollisionEnter(Collision other) {
		Transform item = other.transform;
		if (item.CompareTag("Ground")) {
			StartCoroutine(Enlargen());
		}
	}

	IEnumerator Enlargen() {
		transform.localScale = ogScale; // make bigger
		yield return new WaitForSeconds(.025f);
		cc.isTrigger = true;
		rb.isKinematic = true;
	}
}
