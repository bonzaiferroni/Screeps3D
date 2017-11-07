using UnityEngine;
using System.Collections;

public class  FlyCamera : MonoBehaviour
{
	public Transform target;
	public float distance = 5.0f;
	public float maxDistance = 20;
	public float minDistance = .6f;
	public float xSpeed = 200.0f;
	public float ySpeed = 200.0f;
	public int yMinLimit = -80;
	public int yMaxLimit = 80;
	public int zoomRate = 40;
	public float panSpeed = 0.3f;
	public float zoomDampening = 5.0f;

	private float xDeg = 0.0f;
	private float yDeg = 0.0f;
	private float currentDistance;
	private float desiredDistance;
	private Quaternion currentRotation;
	private Quaternion desiredRotation;
	private Quaternion rotation;
	private Vector3 position;

	void Start() { Init(); }
	void OnEnable() { Init(); }
	public float movementSpeed = 0.2f;
	public float rotationSpeed = 4f;
	public float smoothness = 0.4f;
	public Quaternion targetRotation;
	float targetRotationY;
	float targetRotationX;
	Vector3 targetPosition;
	public void Init()
	{
		//If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
		if (!target)
		{
			GameObject go = new GameObject("Cam Target");
			go.transform.position = transform.position + (transform.forward * distance);
			target = go.transform;
		}

		distance = Vector3.Distance(transform.position, target.position);
		currentDistance = distance;
		desiredDistance = distance;

		//be sure to grab the current rotations as starting points.
		position = transform.position;
		rotation = transform.rotation;
		targetRotation=rotation;
		targetPosition=position;
		currentRotation = transform.rotation;
		desiredRotation = transform.rotation;
		targetRotationY = 180;
		targetRotationX = 10;
		xDeg = Vector3.Angle(Vector3.right, transform.right);
		yDeg = Vector3.Angle(Vector3.up, transform.up);
	}

	 /*
       Camera logic on LateUpdate to only update after all character movement logic has been handled. 
      */
	void LateUpdate()
	{
		// If Control and Alt and Middle button? ZOOM!
		if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl))
		{
			desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate * 0.125f * Mathf.Abs(desiredDistance);
		}
		// If middle mouse and left alt are selected? ORBIT
		else if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt))
		{
			xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
			yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			////////OrbitAngle

			//Clamp the vertical axis for the orbit
			yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
			// set camera rotation 
			desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
			currentRotation = transform.rotation;

			rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
			transform.rotation = rotation;
		}
		// otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
		else if (Input.GetMouseButton(2))
		{
			//grab the rotation of the camera so we can move in a psuedo local XY space
			target.rotation = transform.rotation;
			target.Translate(Vector3.right  * -Input.GetAxis("Mouse X") * panSpeed);
			target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
		}

		////////Orbit Position

		// affect the desired Zoom distance if we roll the scrollwheel
		desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
		//clamp the zoom min/max
		desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
		// For smoothing of the zoom, lerp distance
		currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

		// calculate position based on the new currentDistance 
	//	position = target.position - (rotation * transform.forward * currentDistance  );
		transform.position = position;
 
		if( Input.GetKey( KeyCode.W ) )
			targetPosition += transform.forward * movementSpeed;
		if( Input.GetKey( KeyCode.A ) )
			targetPosition -= transform.right * movementSpeed;
		if( Input.GetKey( KeyCode.S ) )
			targetPosition -= transform.forward * movementSpeed;
		if( Input.GetKey( KeyCode.D ) )
			targetPosition += transform.right * movementSpeed;
		if( Input.GetKey( KeyCode.Q ) )
			targetPosition -= transform.up * movementSpeed;
		if( Input.GetKey( KeyCode.E ) )
			targetPosition += transform.up * movementSpeed;

		if( Input.GetMouseButton( 1 ) )
		{
			Cursor.visible = false;
			targetRotationY += Input.GetAxis( "Mouse X" ) * rotationSpeed;
			targetRotationX -= Input.GetAxis( "Mouse Y" ) * rotationSpeed;
			targetRotation = Quaternion.Euler( targetRotationX, targetRotationY, 0.0f );
		}
		else
			Cursor.visible = true;

	 	transform.position = Vector3.Lerp( transform.position, targetPosition, ( 1.0f - smoothness ) );
		transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, ( 1.0f - smoothness ) );
	}

	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}