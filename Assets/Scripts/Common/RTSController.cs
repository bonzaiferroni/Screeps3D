using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RTSController : MonoBehaviour {

	[SerializeField] private Collider dragCollider;
	private Vector3 posLastFrame;
	private bool dragging;

	// Update is called once per frame
	void Update () {
		if (!Input.GetMouseButton(0)) {
			dragging = false;
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo)) {
			if (hitInfo.collider == dragCollider) {
				Debug.Log("hit!");
				if (dragging) {
					var delta = posLastFrame - hitInfo.point;
					transform.position += delta;
				}
				posLastFrame = hitInfo.point;
				dragging = true;
			}
		}
	}
}
