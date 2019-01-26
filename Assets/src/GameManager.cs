using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject player;
    public Transform bed;
	public float friction = 100f;
    [SerializeField] float gravity = 100f;
    [SerializeField] float maxFallSpeed = 25;

	void Awake() {
		if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player");
	}
}