using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] Transform target;
    private Vector3 distance;
    
    void Start() {
        distance = target.position - transform.position;
        print(distance);
    }

    void Update() {
        transform.position = new Vector3(target.position.x - distance.x, transform.position.y, target.position.z - distance.z);
    }
}