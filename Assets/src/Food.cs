using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

	[SerializeField] float force;
	[SerializeField] float fallSpeed;
	private Rigidbody rb;
	private BoxCollider bc;
	private SphereCollider sc;

	private bool isPicked;
	private bool isFalling;
	private bool isContained;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		bc = GetComponent<BoxCollider>();
		sc = GetComponent<SphereCollider>();
		isPicked = false;
		isFalling = false;
		isContained = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isContained) {
			if (isFalling && !isContained) {
				transform.position = GameManager.instance.bed.position;
				isContained = true;
				sc.isTrigger = false;
				rb.isKinematic = false;
			} else if (isPicked && rb.velocity.y < 0) {
				isFalling = true;
				rb.isKinematic = true;
			}
		}
	}

	public void Pickup() {
		if (!isPicked && !isFalling && !isContained) {
			transform.localScale = new Vector3(.5f, .5f, .5f);
			Physics.IgnoreCollision(GameManager.instance.player.GetComponent<BoxCollider>(), sc);
			transform.position = GameManager.instance.bed.position;
			isContained = true;
			sc.isTrigger = false;
			rb.isKinematic = false;
		}
	}
}
