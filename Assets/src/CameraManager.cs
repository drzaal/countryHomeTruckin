using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private GameObject target;
    private Vector3 distance;
    
    void Start() {
        target = GameManager.instance.player;
        distance = target.transform.position - transform.position;
    }

    void Update() {
        transform.position = new Vector3(target.transform.position.x - distance.x, transform.position.y, target.transform.position.z - distance.z);
    }
}