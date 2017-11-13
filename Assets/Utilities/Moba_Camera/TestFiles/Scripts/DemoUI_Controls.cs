using UnityEngine;
using System.Collections;

public class DemoUI_Controls : MonoBehaviour {
	public Moba_Camera mCam = null;
	public DemoSwitchTarget st = null;
	
	Vector2 currentScrollPosition = Vector2.zero;
	
	public bool showMenu = false;
	
	string camHeight = "";
	
	void Update() {
		if(Input.GetKeyUp(KeyCode.H)) {
			Debug.Log("H");
			if(showMenu) showMenu = false;
			else showMenu = true;
		}
	}
		
	
	void OnGUI() {
		
		if(showMenu == true) {
			currentScrollPosition = GUI.BeginScrollView(new Rect(10, 10, 280, (Screen.height/5)*4),
				currentScrollPosition, new Rect(0, 0, 260, 600));
			
			GUI.Box(new Rect(10, 10, 250, 25), "Moba Camera - created by jkpenner");
			
			GUI.Box(new Rect(10, 40, 250, 250), 
				"Moba Camera Controls: \n" +
				"'L': toggle lock camera to target.\n" +
				"'space': lock camera to target\n" +
				"'Middle Mouse': rotate camera\n" +
				"'Scroll': zoom camera\n" +
				"'Arrow Keys': move camera\n\n" +
				"Move camera when not lock by: \nthe arrow keys or move mouse\nnear edge of screen.\n\n" +
				"Move mouse to rotate camera\nwhen holding the 'Middle Mouse'\n\n" +
				"'H': hide menu\n" +
				"More Controls Below\n");
			
			// Toggle constant rotation
			GUI.Box(new Rect(10, 300, 200, 25), "Toggle Constant Rotation");
			if(mCam.settings.rotation.constRotationRate) {
				if(GUI.Button(new Rect(210, 300, 50, 25), "On")) {
					mCam.settings.rotation.constRotationRate = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 300, 50, 25), "Off")) {
					mCam.settings.rotation.constRotationRate = true;
				}
			}
			
			// Toggle constant zoom
			GUI.Box(new Rect(10, 330, 200, 25), "Toggle Constant Zoom");
			if(mCam.settings.zoom.constZoomRate) {
				if(GUI.Button(new Rect(210, 330, 50, 25), "On")) {
					mCam.settings.zoom.constZoomRate = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 330, 50, 25), "Off")) {
					mCam.settings.zoom.constZoomRate = true;
				}
			}
			
			// Cycle through targets
			GUI.Box(new Rect(10, 360, 150, 25), "Switch Target");
			if(GUI.Button(new Rect(160, 360, 50, 25), "+")) {
				st.changeTargetUp();
			}
			if(GUI.Button(new Rect(210, 360, 50, 25), "-")) {
				st.changeTargetDown();	
			}
			
			// Toggle Boundaries
			GUI.Box(new Rect(10, 390, 200, 25), "Toggle Boundaries");
			if(mCam.settings.useBoundaries) {
				if(GUI.Button(new Rect(210, 390, 50, 25), "On")) {
					mCam.settings.useBoundaries = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 390, 50, 25), "Off")) {
					mCam.settings.useBoundaries = true;
				}
			}
			
			// Toggle Rotation Lock X
			GUI.Box(new Rect(10, 420, 200, 25), "Toggle Rotation X Lock:");
			if(mCam.settings.rotation.lockRotationX) {
				if(GUI.Button(new Rect(210, 420, 50, 25), "On")) {
					mCam.settings.rotation.lockRotationX = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 420, 50, 25), "Off")) {
					mCam.settings.rotation.lockRotationX = true;
				}
			}
			
			// Toggle Rotation Lock Y
			GUI.Box(new Rect(10, 450, 200, 25), "Toggle Rotation Y Lock:");
			if(mCam.settings.rotation.lockRotationY) {
				if(GUI.Button(new Rect(210, 450, 50, 25), "On")) {
					mCam.settings.rotation.lockRotationY = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 450, 50, 25), "Off")) {
					mCam.settings.rotation.lockRotationY = true;
				}
			}
			
			
			
			// Cycle through targets
			GUI.Box(new Rect(10, 480, 100, 25), "Camera Height");
			if(GUI.Button(new Rect(110, 480, 50, 25), "+")) {
				++mCam.settings.movement.defualtHeight;
			}
			camHeight = mCam.settings.movement.defualtHeight.ToString();
			GUI.TextField(new Rect(160, 480, 50, 25), camHeight);
			if(GUI.Button(new Rect(210, 480, 50, 25), "-")) {
				--mCam.settings.movement.defualtHeight;
			}
			
			GUI.Box(new Rect(10, 510, 200, 25), "Set Height to Target(locked):");
			if(mCam.settings.movement.useLockTargetHeight) {
				if(GUI.Button(new Rect(210, 510, 50, 25), "On")) {
					mCam.settings.movement.useLockTargetHeight = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 510, 50, 25), "Off")) {
					mCam.settings.movement.useLockTargetHeight = true;
				}
			}
			
			// Edge Hover movement
			GUI.Box(new Rect(10, 540, 200, 25), "Camera move on edge hover:");
			if(mCam.settings.movement.edgeHoverMovement) {
				if(GUI.Button(new Rect(210, 540, 50, 25), "On")) {
					mCam.settings.movement.edgeHoverMovement = false;	
				}
			}
			else {
				if(GUI.Button(new Rect(210, 540, 50, 25), "Off")) {
					mCam.settings.movement.edgeHoverMovement = true;
				}
			}
			
			if(GUI.Button(new Rect(10, 570, 250, 25), "Reset")) {
				mCam.settings.rotation.lockRotationY = false;
				mCam.settings.rotation.lockRotationX = true;
				mCam.SetCameraZoom(mCam.settings.zoom.defaultZoom);
				mCam.SetCameraRotation(-45, 0);
				mCam.settings.useBoundaries = true;
				mCam.settings.rotation.constRotationRate = false;
				mCam.settings.zoom.constZoomRate = false;	
				mCam.settings.movement.defualtHeight = 5;
				mCam.settings.movement.useLockTargetHeight = true;
				mCam.settings.movement.edgeHoverMovement = true;
			}
			
			
			GUI.EndScrollView();
		}
		else {
			GUI.Box(new Rect(20, 20, 250, 25), "'H': Show Menu");
		}
	}
}
