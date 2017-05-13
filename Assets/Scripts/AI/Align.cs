using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour 
{
	GameObject character;
	[SerializeField] GameObject target;

	[SerializeField] float maxAngularAcceleration = 100f;
	[SerializeField] float maxRotation = 100f;

	[SerializeField] float targetRadius = .1f;
	[SerializeField] float slowRadius = 1f;

	[SerializeField] float timeToTarget = .1f;

	float rotation;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate()
	{
		SteeringOutput acceleration = GetAcceleration();

		if(acceleration != null)
		{
			rotation += acceleration.angular * Time.fixedDeltaTime;

			if(rotation > maxRotation)
			{
				rotation = Math.Sign(rotation) * maxAngularAcceleration;
			}

			character.transform.rotation = Quaternion.Euler(
				character.transform.rotation.eulerAngles.x, 
				character.transform.rotation.eulerAngles.y + (rotation * Time.fixedDeltaTime),
				character.transform.rotation.eulerAngles.z
			);
		}
		else
		{
			rotation = 0f;
		}
	}

	SteeringOutput GetAcceleration()
	{
		float currentRotation = 0f;
		float targetRotation = 0f;
		float currentRotationSize = 0f;

		SteeringOutput acceleration = new SteeringOutput();

		// Direction we are facing and direction we want to face
		Vector3 characterOrientation = character.transform.forward;
		Vector3 targetOrientation = target.transform.position - character.transform.position;

		// Map the directions to degree orientations, subtract as degrees, then convert back to radians (-Pi, Pi]
		currentRotation = Math.MapPiToDegrees(Mathf.Atan2(targetOrientation.x, targetOrientation.z)) - 
			Math.MapPiToDegrees(Mathf.Atan2(characterOrientation.x, characterOrientation.z));
		currentRotation = Math.MapDegreesToPi(currentRotation);

		// Get the magnitude of the rotation
		currentRotationSize = Mathf.Abs(currentRotation);

		if(currentRotationSize < targetRadius)
		{
			return null;
		}
		else if(currentRotationSize > slowRadius)
		{
			targetRotation = maxRotation;	
		}
		else
		{
			targetRotation = maxRotation * currentRotationSize / slowRadius;
		}

		targetRotation *= Math.Sign(currentRotation);

		acceleration.angular = targetRotation - rotation;
		acceleration.angular /= timeToTarget;

		if(Mathf.Abs(acceleration.angular) > maxAngularAcceleration)
		{
			acceleration.angular /= Mathf.Abs(acceleration.angular);
			acceleration.angular *= maxAngularAcceleration;
		}

		acceleration.linear = Vector3.zero;

		return acceleration;
	}
}
