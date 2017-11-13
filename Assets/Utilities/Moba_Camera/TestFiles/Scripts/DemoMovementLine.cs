using UnityEngine;
using System.Collections;

public class DemoMovementLine : MonoBehaviour {
	public float vx = 0.1f;
	public int dir = 1;
	public float maxDistFromStart = 10.0f;
	private Vector3 centerPos;
	
	
	// Use this for initialization
	void Start () {
		centerPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.forward * vx * dir;
		
		if((transform.position - centerPos).magnitude > maxDistFromStart) {
			transform.position = transform.forward * dir * maxDistFromStart + centerPos;
			dir *= -1;
		}
		/*
		if(transform.position > centerPos + maxDistFromStart) {
			transform.position = new Vector3(centerPos.x + maxDistFromStart, transform.position.y, transform.position.z);
			dir *= -1;
		}
		else if(transform.position.x < centerPos.x - maxDistFromStart) {
			transform.position = new Vector3(centerPos.x - maxDistFromStart, transform.position.y, transform.position.z);
			dir *= -1;
		}
		*/
	}
}
