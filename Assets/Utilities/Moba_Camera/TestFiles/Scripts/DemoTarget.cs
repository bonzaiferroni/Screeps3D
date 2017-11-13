using UnityEngine;
using System.Collections;

public class DemoTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("DemoManager");
		if(go) {
			DemoSwitchTarget dst = go.GetComponent<DemoSwitchTarget>();
			if(dst) {
				dst.AddTarget(this.transform);				
			}
		}
	}
}
