﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour {

	// [SerializeField] float maxSpeed;
	[SerializeField] float accelerateSpeed;
	[SerializeField] float reverseSpeed;
	[SerializeField] float turnSpeed;
	[SerializeField] float linearDrag;
	[SerializeField] float angularDrag;
	[SerializeField] float maxDepenetrationVelocity;
	[SerializeField] float maxSpeed;

	public Vector3 velocity;
	public Vector3 angularVelocity;

	// [SerializeField] float accelerationTime;
	// [SerializeField] float reverseTime;

	// [SerializeField] AnimationCurve accelerationCurve;
	// [SerializeField] AnimationCurve reverseCurve;
	[SerializeField] AnimationCurve curve;

	// [SerializeField] Transform bed;
	[SerializeField] Transform possessions;
	[SerializeField] ParticleSystem particlePuffer;
	[SerializeField] Transform wheels;
	
	private Rigidbody rb;
	// private Vector3 velocity;
	// private float isAccelerating;
	// private float isReversing;
	private float crashTime;
	private bool destruction;

	private float xmove;
	private float zmove;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		// velocity = new Vector3(0, 0, 0);
		// isAccelerating = 0;
		// isReversing = 0;
		crashTime = Time.time;
		destruction = false;
	}

	IEnumerator TurnWheels(float direction) {
		float origin = wheels.GetChild(0).rotation.x;
		float destination = origin + 20 * direction;
		float time = .5f;
		float startTime = Time.time;
		float timeDelta = 0;
		float evaluate;

		while (timeDelta < 1) {
			timeDelta = Time.time - startTime;
			if (timeDelta < time) {
				timeDelta = timeDelta / time;
			} else {
				timeDelta = 1;
			}

			evaluate = curve.Evaluate(timeDelta);
			float rotation = (destination - wheels.GetChild(0).rotation.x) * evaluate + wheels.GetChild(0).rotation.x;
			// wheels.GetChild(0).rotation = rotation;
			//wheels.GetChild(0).Rotate(rotation, 0, 0);
			yield return new WaitForEndOfFrame();
		}
	}

	/* IEnumerator UnturnWheels() {
		float origin = wheels.GetChild(0).rotation.x;
		float destination = origin + 20 * direction;
		float time = .5f;
		float startTime = Time.time;
		float timeDelta = 0;
		float evaluate;

		while (timeDelta < 1) {
			timeDelta = Time.time - startTime;
			if (timeDelta < time) {
				timeDelta = timeDelta / time;
			} else {
				timeDelta = 1;
			}

			evaluate = curve.Evaluate(timeDelta);
			float rotation = (destination - wheels.GetChild(0).rotation.x) * evaluate + wheels.GetChild(0).rotation.x;
			// wheels.GetChild(0).rotation = rotation;
			wheels.GetChild(0).Rotate(rotation, 0, 0);
			yield return new WaitForEndOfFrame();
		}
	} */

	/* private IEnumerator Tween(GameObject obj, float yCoord, AnimationCurve curve, float time) {
		Item item = obj.GetComponent<Item>();
		Vector3 move;
		float moveX;
		float moveY;
		float startTime = Time.time;
		float timeDelta = 0;
		float evaluate;

		while (timeDelta < 1) {
			float top = bc.bounds.max.y;
			Vector3 reallocation = new Vector3(transform.position.x, top + yCoord, obj.transform.position.z);
			timeDelta = Time.time - startTime;
			if (item.beingHeld && timeDelta < time) {
				timeDelta = timeDelta / time;
			} else {
				timeDelta = 1;
			}

			evaluate = curve.Evaluate(timeDelta);
			moveX = (reallocation.x - obj.transform.position.x) * evaluate + obj.transform.position.x;
			moveY = (reallocation.y - obj.transform.position.y) * evaluate + obj.transform.position.y;
			move = new Vector3(moveX, moveY, reallocation.z);
			obj.transform.position = move;

			yield return new WaitForEndOfFrame();
		}

		/* obj.GetComponent<SpriteRenderer>().sortingLayerName = "Set Blocks";
		spawnParticles(location);
		StartCoroutine(Camera.main.GetComponent<Shake>().ShakeCamera());
        this.audioSource.PlayOneShot(sndBoom, 0.9f);
	} */
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance.haveWon)
		{
			zmove = 0;
			xmove = 0;
		}
		else
		{
			DriveTruck();
		}

		angularVelocity = rb.angularVelocity;
		velocity = rb.velocity;
		Debug.Log(2 * Mathf.PI * xmove * turnSpeed);
	}

	void DriveTruck()
	{
		xmove = Input.GetAxisRaw("Horizontal");
		zmove = Input.GetAxisRaw("Vertical");

		// handle rotation (y axis)
			//transform.Rotate(0, xmove * turnSpeed * Time.deltaTime, 0);

		if (xmove != 0) {
			StartCoroutine(TurnWheels(xmove));
		} else {
			// StartCoroutine(UnturnWheels());
		}

		foreach (Transform wheel in wheels) {
			// TRANSFORM ISSUE WITH THE MODEL
			// wheel.Rotate(-zmove * (Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z)) * 2 * Time.deltaTime, 0, 0);
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

	void FixedUpdate() {
		rb.drag = linearDrag;
		rb.maxDepenetrationVelocity = maxDepenetrationVelocity;
		rb.angularDrag = angularDrag;

		// handle forward and backward (z axis) movement
		if (zmove != 0) {
			// print(rb.velocity);
			if (zmove > 0) {
				rb.AddForce(transform.forward * accelerateSpeed, ForceMode.VelocityChange);
			} else if (zmove < 0) {
				rb.AddForce(-transform.forward * reverseSpeed, ForceMode.VelocityChange);
			}
		} else {
			// print(rb.velocity);

			//rb.velocity = rb.velocity * 0.975f;
		}

		if (Mathf.Abs(velocity.x) > 0.001f || Mathf.Abs(velocity.z) > 0.001f) {
			rb.AddTorque(new Vector3(0f, 2f * Mathf.PI * xmove * turnSpeed, 0f), ForceMode.Acceleration);
		}
		//rb.AddTorque(new Vector3(0f, 20f, 0f), ForceMode.VelocityChange);
 		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}

	void LateUpdate() {
		// transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
	}

	void Accelerate() {
		
	}

	void Reverse() {

	}

	void Decelerate() {

	}

	void PuffAtLocation(Vector3 location)
	{
		particlePuffer.transform.position = location;
		particlePuffer.Play();
	}

	void OnCollisionEnter(Collision other) {
		Transform item = other.transform;
		if (item.CompareTag("Obstacle")) {
			if (Mathf.Abs(velocity.x) > 15 || Mathf.Abs(velocity.z) > 15) {
				float temp = Time.time;
				if (temp - crashTime > 3) {
					crashTime = temp;
					DropItems();
					if (particlePuffer != null && other.contactCount > 0)
					{
						PuffAtLocation(other.contacts[0].point);
					}

					AudioSource sfx;
					if ((sfx = item.GetComponent<AudioSource>()) != null)
					{
						sfx.Play();
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Transform item = other.transform;

		if (item.CompareTag("Food")) {
			Vector3 oldScale = item.transform.lossyScale;
			item.parent = possessions;
			
			item.GetComponent<Food>().Pickup();
			Pickup();

			AudioSource sfx;
			if ((sfx = item.GetComponent<AudioSource>()) != null)
			{
				sfx.Play();
			}
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
			GameManager.instance.mealNutrition = GameManager.instance.levelStats.toCubeRtSum();
			GameManager.instance.foodSum = GameManager.instance.levelStats.toSumPoints();

		}
	}

	void Pickup() {

		CountFoods();
	}

	void DropItems() {
		if (possessions.childCount > 0) {
			int toDrop = (int) Mathf.Min(6, Mathf.Max(1, Mathf.Floor(possessions.childCount / 3)));
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
