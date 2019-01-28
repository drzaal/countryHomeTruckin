using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField] float force;
	[SerializeField] float fallSpeed;
	[SerializeField] float baseScale;

	public enum FoodType { PIG, PUMPKIN, CORN, COW, TURKEY, CABBAGE, POTATO }

	[SerializeField] public FoodType foodType;

	private Animator animator;
	private Rigidbody rb;
	private CapsuleCollider cc;
	private Vector3 ogScale;
	private bool isContained;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<CapsuleCollider>();
		isContained = false;

		if (animator) {
			StartCoroutine(StartAnimation());
		}
	}

	IEnumerator StartAnimation() {
		animator.enabled = false;
		yield return new WaitForSeconds(Random.Range(0f, 1f));
		animator.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Pickup() {
		if (!isContained) {
			Vector3 scale = new Vector3(
				1f / transform.parent.lossyScale.x,
				1f / transform.parent.lossyScale.y,
				1f / transform.parent.lossyScale.z) * baseScale;

			transform.localScale = scale ; // make smaller
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
		isContained = false;
		transform.parent = GameManager.instance.food;
		transform.localScale = baseScale * Vector3.one; // make bigger
		gameObject.layer = 0; // change layer for collisions
		yield return new WaitForSeconds(.025f);
		cc.isTrigger = true;
		rb.isKinematic = true;
	}
}
