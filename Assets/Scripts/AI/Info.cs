using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour 
{
	public Vector3 velocity;
	[SerializeField] float magnitude;

	Vector3 lastPosition = Vector3.zero;
	Vector3 currentPosition = Vector3.zero;

	void FixedUpdate () 
	{
		currentPosition = transform.position;

		velocity = GetVelocity(currentPosition, lastPosition, Time.fixedDeltaTime);
		magnitude = velocity.magnitude;

		lastPosition = transform.position;
	}

	Vector3 GetVelocity(Vector3 curPos, Vector3 lastPos, float timeDelta)
	{
		Vector3 velocityDirection = curPos - lastPos;
		float distanceTraveled = velocityDirection.magnitude;
		float velocityMagnitude = distanceTraveled / timeDelta;

		return velocityDirection.normalized * velocityMagnitude;
	}
}