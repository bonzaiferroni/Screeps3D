using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

	[SerializeField] private Vector3 rotation;
	[SerializeField] private bool mutateRandomly;
	private Vector3 mutator;
	
	// Update is called once per frame
	void Update () {
		transform.rotation = transform.rotation * Quaternion.Euler(rotation * Time.deltaTime);
		if (mutateRandomly && Random.value < .1) {
			mutator.x += (Random.value * .01f) - .005f;
			mutator.y += (Random.value * .01f) - .005f;
			mutator.z += (Random.value * .01f) - .005f;
			mutator.x = Mathf.Clamp(mutator.x, -.1f, .1f);
			mutator.y = Mathf.Clamp(mutator.y, -.1f, .1f);
			mutator.z = Mathf.Clamp(mutator.z, -.1f, .1f);
			rotation = rotation + mutator;
			rotation.x = Mathf.Clamp(rotation.x, -1f, 1f);
			rotation.y = Mathf.Clamp(rotation.y, -1f, 1f);
			rotation.z = Mathf.Clamp(rotation.z, -1f, 1f); 
		}
	}
}
