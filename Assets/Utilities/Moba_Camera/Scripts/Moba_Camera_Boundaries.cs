/*
 * Moba Camera Boundaries Script v1.1 
 * - created by Jacob Penner 2013
 * 
 * Notes: 
 * - Cube boundarys 
 *   - only support rotation on the Y-axis.
 *   - don't restrict movement on the Y-axis
 * - Sphere boundarys
 *   - only support uniform scale (takes the x scale value)
 * 
 * Plans:
 *  - Add non-uniform scale to the sphere boundaries
 *  - Add option to restrict cube movement on Y-axis
 * 
 * Version:
 * v1.1 
 * - Support for sphere boundary
 *   - Uniform scale only
 * - Support for cube boundary
 *   - rotation on the Y-axis only
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Moba_Camera_Boundaries {
	// Layer for boundaries
	static public string boundaryLayer = "mobaCameraBoundaryLayer";
	static private bool boundaryLayerExists = true;
	
	// List containing the boundaries in the scene
	static private List<Moba_Camera_Boundary> cube_boundaries 	= new List<Moba_Camera_Boundary>();
	static private List<Moba_Camera_Boundary> sphere_boundaries = new List<Moba_Camera_Boundary>();
	
	public enum BoundaryType { cube, sphere, none };
	
	// returns number of boundaries in the lists
	static public int GetNumberOfBoundaries() {
		return cube_boundaries.Count + sphere_boundaries.Count;
	}
	
	// add boundary to list 
	static public bool AddBoundary(Moba_Camera_Boundary boundary, BoundaryType type) {
		if(boundary == null) {
			Debug.LogWarning("Name: " + boundary.name + "; Error: AddBoundary() - null boundary passed");
			return false;
		}
		
		if(type == BoundaryType.cube) {
			cube_boundaries.Add(boundary);
			return true;
		}
		else if(type == BoundaryType.sphere) {
			sphere_boundaries.Add(boundary);
			return true;
		}
		else {
			Debug.LogWarning ("Name: " + boundary.name + "; Error: AddBoundary() - Incorrect BoundaryType, boundary will not be used");
			return false;
		}
	}
	
	// Remove a boundary from the list
	static public bool RemoveBoundary(Moba_Camera_Boundary boundary, BoundaryType type) {
		if(type == BoundaryType.cube) {
			return cube_boundaries.Remove(boundary);	
		}
		else if(type == BoundaryType.sphere) {
			return cube_boundaries.Remove(boundary);
		}
		else {
			return false;	
		}
	}
	
	static public void SetBoundaryLayerExist(bool value) {
		if(boundaryLayerExists) {
			boundaryLayerExists = false;
			Debug.LogWarning("LayerMask not set for Moba_Camera_Boundaries. Add new Layer named " + boundaryLayer + ". Check Read me for more information on recommended settings.");	
		}
	}
	
	
	// returns the distance from the center of the box collider based on the box rotations
	static Vector3 calBoxRelations(BoxCollider box, Vector3 point, bool containedToBox, out bool isPointInBox) {
		Vector3 center = box.transform.position + box.center;
		
		// Cube Size Information
		float hWidth 	= box.size.x/2.0f * box.transform.localScale.x;
		float hHeight 	= box.size.y/2.0f * box.transform.localScale.y;
		float hDepth 	= box.size.z/2.0f * box.transform.localScale.z;
		
		float yt = Vector3.Dot((point - center), box.transform.up);
		Vector3 pointOffsetY = point + yt * -box.transform.up;
		
		float xt = Vector3.Dot((pointOffsetY - center), box.transform.right);
		Vector3 pointOffsetX = pointOffsetY + xt * -box.transform.right;
		
		Vector3 zVec = pointOffsetX - center;
		Vector3 yVec = point - pointOffsetY;
		Vector3 xVec = pointOffsetY - pointOffsetX;
		
		float zDist = zVec.magnitude;
		float yDist = yVec.magnitude;
		float xDist = xVec.magnitude;
	
		isPointInBox = true;
		if(zDist > hDepth) {
			if(containedToBox)
				zDist = hDepth;
			isPointInBox = false;
		}
		if(yDist > hHeight) {
			if(containedToBox)
				yDist = hHeight;
			isPointInBox = false;
		}
		if(xDist > hWidth) {
			if(containedToBox)
				xDist = hWidth;
			isPointInBox = false;
		}
			
		zDist *= ((Vector3.Dot(box.transform.forward, zVec) >= 0.0f)?(1.0f):(-1.0f));
		yDist *= ((Vector3.Dot(box.transform.up, yVec) >= 0.0f)?(1.0f):(-1.0f));
		xDist *= ((Vector3.Dot(box.transform.right, xVec) >= 0.0f)?(1.0f):(-1.0f));
		
		return new Vector3(xDist, yDist, zDist);
	}
	
	static Vector3 getClosestPointOnSurfaceBox(BoxCollider box, Vector3 point) {
		bool isIn;
		Vector3 dists = calBoxRelations(box, point, true, out isIn);
		return box.transform.position + box.transform.forward * dists.z +
				box.transform.right * dists.x + box.transform.up * dists.y;
	}
	
	
	// Check if a give point in within any boundary contained in the list
	static public bool isPointInBoundary(Vector3 point) {
		bool pointIsInBoundary = false;
		// loop through each cube boundary
		foreach(Moba_Camera_Boundary boundary in cube_boundaries) {
			// check if the boundary is not active. if true, skip it.
			if(boundary.isActive == false) continue;
			BoxCollider boxCollider = boundary.GetComponent<BoxCollider>();
			if(boxCollider == null) {
				Debug.LogWarning("Boundary: " + boundary.name + "; Error: BoundaryType and Collider mismatch."); 	
				continue;
			}
			bool pointIsIn;
			calBoxRelations(boxCollider, point, false, out pointIsIn);
			
			if(pointIsIn) {
				pointIsInBoundary = true;	
			}
		}
		
		// loop through each sphere boundary
		foreach(Moba_Camera_Boundary boundary in sphere_boundaries) {
			// check if the boundary is not active. if true, skip it.
			if(boundary.isActive == false) continue;
			
			SphereCollider sphereCollider = boundary.GetComponent<SphereCollider>();
			if(sphereCollider == null) {
				Debug.LogWarning("Boundary: " + boundary.name + "; Error: BoundaryType and Collider mismatch.");
				continue;	
			}
			// check if the distance from the center of the boundary to the point is less then the radius
			if((boundary.transform.position + sphereCollider.center - point).magnitude < sphereCollider.radius) {
				pointIsInBoundary = true;
			}
		}
		return pointIsInBoundary;
	}
	
	
	static public Moba_Camera_Boundary GetClosestBoundary(Vector3 point) {
		// Contains the info for the closest boundary
		Moba_Camera_Boundary closestBoundary = null;
		float closestDistance = 999999.0f;
		
		// if pivot is outside the boundries find the closest cube
		foreach(Moba_Camera_Boundary boundary in cube_boundaries) {
			if(boundary == null) continue;
			if(boundary.isActive == false) continue;
			
			BoxCollider boxCollider = boundary.GetComponent<BoxCollider>();
			Vector3 pointOnSurface = getClosestPointOnSurfaceBox(boxCollider, point);
			
			float distance = (point-pointOnSurface).magnitude;
			
			// if the distance is closer calculate the point and set
			if(distance < closestDistance) {
				closestBoundary = boundary;
				closestDistance = distance;
			}
		}
		
		foreach(Moba_Camera_Boundary boundary in sphere_boundaries) {
			if(boundary.isActive == false) continue;
			
			SphereCollider sphereCollider = boundary.GetComponent<SphereCollider>();
			
			Vector3 center 	= boundary.transform.position + sphereCollider.center;
			float radius 	= sphereCollider.radius;
			
			Vector3 centerToPoint = point - center;
			
			Vector3 pointOnSurface = center + (centerToPoint.normalized * radius);
			
			// the distance from the center of the sphere to the posiiton
			float distance 	= (point - pointOnSurface).magnitude;
			
			// check if it's the closest point
			if(distance < closestDistance) {
				closestBoundary = boundary;
				closestDistance = distance;
			}
		}
		
		return closestBoundary;
	}
	
	static public Vector3 GetClosestPointOnBoundary(Moba_Camera_Boundary boundary, Vector3 point) {
		Vector3 pointOnBoundary = point;
		
		// Find the closest point on the boundary depending on type of boundary
		if(boundary.type == BoundaryType.cube) {
			BoxCollider boxCollider = boundary.GetComponent<BoxCollider>();

			pointOnBoundary = getClosestPointOnSurfaceBox(boxCollider, point);
		}
		else if(boundary.type == BoundaryType.sphere) {
			SphereCollider sphereCollider = boundary.GetComponent<SphereCollider>();
			
			Vector3 center 	= boundary.transform.position + sphereCollider.center;
			float radius 	= sphereCollider.radius;

			Vector3 centerToPosition = point - center;
			
			// Get point on surface of the sphere
			pointOnBoundary = center + (centerToPosition.normalized * radius);
		}
		
		return 	pointOnBoundary;
	}
}
