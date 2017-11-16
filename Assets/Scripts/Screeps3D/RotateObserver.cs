using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObserver : MonoBehaviour {
	private const float maxRandomDelta = 80;
	private const float targetDelay = 1;
	
	private float nextTarget;
	private Quaternion[] rotations;
	private Quaternion target;

	// Use this for initialization
	void Start () {
		rotations = new Quaternion[5];
		var initial = transform.rotation.eulerAngles;
		for (var i = 0; i < 5; i++) {
			rotations[i] = Quaternion.Euler(Randomize(initial.x), Randomize(initial.y), initial.z);
		} 
	}
	
	// Update is called once per frame
	void Update () {
		FindNewTarget();
		RotateTowardTarget();
	}

	private void RotateTowardTarget() {
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
	}

	private void FindNewTarget() {
		if (nextTarget > Time.time) return;
		nextTarget = Time.time + targetDelay;
		target = rotations[(int) (rotations.Length * Random.value)];
	}

	private float Randomize(float value) {
		return value + (Random.value * maxRandomDelta * 2) - maxRandomDelta;
	}
}
