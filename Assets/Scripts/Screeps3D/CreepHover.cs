using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepHover : MonoBehaviour {
	private Vector3 targetPosition;
	private Quaternion targetRotation;
	private Vector3 posRef;
	private float nextRotationTarget;
	private float nextPosTarget;

	private const float posDrift = .025f;
	private const float rotDrift = 5;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FindNewPosTarget();
		FindNewRotationTarget();
		ApproachTarget();
	}

	private void FindNewRotationTarget() {
		if (nextRotationTarget > Time.time) return;
		nextRotationTarget = Time.time + Random.value * 2;
		targetRotation = Quaternion.Euler(RandomDrift(rotDrift), RandomDrift(rotDrift), RandomDrift(rotDrift));
	}

	private void FindNewPosTarget() {
		if (nextPosTarget > Time.time) return;
		nextPosTarget = Time.time + Random.value;
		targetPosition = new Vector3(RandomDrift(posDrift), RandomDrift(posDrift), RandomDrift(posDrift));
	}

	private void ApproachTarget() {
		transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref posRef, 1);
		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime);
	}

	private float RandomDrift(float drift) {
		return (Random.value * drift * 2) - drift;
	}
}
