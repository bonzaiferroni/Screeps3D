using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonMoveController : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private AnimationTrigger trigger;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		trigger = GetComponent<AnimationTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		if (EventSystem.current.currentSelectedGameObject) {
			return;
		}
		
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveDirection;
			moveDirection *= speed;
			var magnitude = moveDirection.magnitude;
			if (magnitude > 0) {
				transform.rotation = Quaternion.LookRotation(moveDirection);
			}
			if (trigger) {
				trigger.SetSpeed(magnitude);
			}
			if (Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
				if (trigger) {
					trigger.TriggerJump();
				}
			}
		} else {
			moveDirection.y -= gravity * Time.deltaTime;
		}
	
		controller.Move(moveDirection * Time.deltaTime);
	}
}
