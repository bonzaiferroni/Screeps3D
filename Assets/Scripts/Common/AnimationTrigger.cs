using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour {
	private Animator anim;
	private int speedHash;
	private int jumpHash;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		foreach (var parameter in anim.parameters) {
			if (parameter.name == "Speed") {
				speedHash = parameter.nameHash;
			}
			if (parameter.name == "Jump") {
				jumpHash = parameter.nameHash;
			}
		}
	}

	public void SetSpeed(float speed) {
		if (speedHash == 0) {
			return; 
		}
		anim.SetFloat(speedHash, speed);
	}
	
	public void TriggerJump() {
		if (jumpHash == 0) {
			return;
		}
		anim.SetTrigger(jumpHash);
	}
}
