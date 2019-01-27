﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {

	// [SerializeField] float maxSpeed;
	[SerializeField] float accelerateSpeed;
	[SerializeField] float reverseSpeed;
	[SerializeField] float turnSpeed;

	public Vector3 velocity;

	// [SerializeField] float accelerationTime;
	// [SerializeField] float reverseTime;

	// [SerializeField] AnimationCurve accelerationCurve;
	// [SerializeField] AnimationCurve reverseCurve;

	// [SerializeField] Transform bed;
	[SerializeField] Transform possessions;

	private Rigidbody rb;
	// private Vector3 velocity;
	// private float isAccelerating;
	// private float isReversing;
	private float crashTime;
	private bool destruction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		// velocity = new Vector3(0, 0, 0);
		// isAccelerating = 0;
		// isReversing = 0;
		crashTime = Time.time;
		destruction = false;
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

		velocity = rb.velocity;

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

	void OnCollisionEnter(Collision other) {
		Transform item = other.transform;
		if (item.CompareTag("Obstacle")) {
			if (Mathf.Abs(velocity.x) > 15 || Mathf.Abs(velocity.z) > 15) {
				float temp = Time.time;
				if (temp - crashTime > 3) {
					crashTime = temp;
					DropItems();
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Transform item = other.transform;
		if (item.CompareTag("Food")) {
			item.transform.localScale = new Vector3(.5f, .5f, .5f);
			item.parent = possessions;
			item.GetComponent<Food>().Pickup();
			Pickup();
		}

		if (other.transform.CompareTag("HomeZone") && GameManager.instance != null)
		{
			GameManager.instance.winLevel();
		}
    }

	void CountFoods()
	{
if (possessions.childCount > 0) {
			Food[] foods = possessions.GetComponentsInChildren<Food>();
			if (foods == null) throw new System.Exception();

			int pumpkins = 0;
			int pigs = 0;
			int cows = 0;
			int turkeys = 0;
			int chickens = 0;
			int potatoes = 0;
			int corn = 0;
			int cabbage = 0;

			foreach (Food food in foods)
			{
				switch(food.foodType)
				{
					case Food.FoodType.PUMPKIN: pumpkins++; break;
					case Food.FoodType.PIG: pigs++; break;
					case Food.FoodType.TURKEY: turkeys++; break;
					case Food.FoodType.POTATO: potatoes++; break;
					case Food.FoodType.CABBAGE: cabbage++; break;
					case Food.FoodType.CORN: corn++; break;
					case Food.FoodType.COW: cows++; break;
				}
			}
			GameManager.instance.levelStats = new LevelStats
			{
				pumpkins = pumpkins,
				pigs = pigs,
				corn = corn,
				cows = cows,
				cabbages = cabbage,
				potatoes = potatoes,
				turkeys = turkeys
			};

			Debug.Log(JsonUtility.ToJson( GameManager.instance.levelStats));

		}
	}

	void Pickup() {

		CountFoods();
	}

	void DropItems() {
		if (possessions.childCount > 0) {
			int toDrop = (int) Mathf.Max(1, Mathf.Floor(possessions.childCount / 3));
			while (toDrop > 0) {
				Transform child = possessions.GetChild(Random.Range(0, possessions.childCount));
				child.parent = GameManager.instance.food;
				child.GetComponent<Food>().Throw();
				toDrop -= 1;
				if (toDrop > possessions.childCount) {
					toDrop = 0;
				}
			}
		}
		CountFoods();
	}
}
