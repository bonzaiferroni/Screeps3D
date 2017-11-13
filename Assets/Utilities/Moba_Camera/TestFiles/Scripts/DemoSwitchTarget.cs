using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoSwitchTarget : MonoBehaviour {
	public Moba_Camera moba_camera = null;
	
	public List<Transform> targets = new List<Transform>();
	public int currentTarget = 0;
	
	// Use this for initialization
	void Start () {
		if(!moba_camera) this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Equals)) {
			changeTargetUp();
		}
		else if(Input.GetKeyDown(KeyCode.Minus)) {
			changeTargetDown();
		}
		
		moba_camera.SetTargetTransform(targets[currentTarget]);
	}
	
	public void changeTargetUp() {
		++currentTarget;
		if(currentTarget >= targets.Count) currentTarget = 0;
	}
	
	public void changeTargetDown() {
		--currentTarget;
		if(currentTarget < 0) currentTarget = targets.Count - 1;
	}
	
	public void AddTarget(Transform target) {
		if(!targets.Contains(target)) {
			targets.Add(target);
		}
	}
}
