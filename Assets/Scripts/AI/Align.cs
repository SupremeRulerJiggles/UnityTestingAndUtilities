using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour 
{
	protected GameObject character;
	protected GameObject target;

	[SerializeField] protected float maxAngularAcceleration = 100f;
	[SerializeField] protected float maxRotation = 100f;

	[SerializeField] protected float targetRadius = .1f;
	[SerializeField] protected float slowRadius = 1f;

	[SerializeField] protected float timeToTarget = .1f;

	protected float rotation;

	// GetAcceleration Variables
	protected float rotationRemaining;
	protected float rotationRemainingSize;
	protected float targetRotation;
	protected SteeringOutput acceleration;
	protected Vector3 characterForward;
	protected Vector3 targetForward;

	protected void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	protected void FixedUpdate()
	{
		GetForwards();

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

	protected virtual SteeringOutput GetAcceleration()
	{
		acceleration = new SteeringOutput();

		// Map the forward directions to degree orientations, subtract the forward orientations to get the
		// rotational difference between them, then convert that difference back to radians (-Pi, Pi]
		rotationRemaining = Math.MapPiToDegrees(Mathf.Atan2(targetForward.x, targetForward.z)) - 
			Math.MapPiToDegrees(Mathf.Atan2(characterForward.x, characterForward.z));
		rotationRemaining = Math.MapDegreesToPi(rotationRemaining);

		// Get the magnitude of the rotation
		rotationRemainingSize = Mathf.Abs(rotationRemaining);

		if(rotationRemainingSize < targetRadius)
		{
			return null;
		}
		else if(rotationRemainingSize > slowRadius)
		{
			targetRotation = maxRotation;	
		}
		else
		{
			targetRotation = maxRotation * rotationRemainingSize / slowRadius;
		}

		targetRotation *= Math.Sign(rotationRemaining);

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

	protected virtual void GetForwards()
	{
		// Direction we are facing and direction we want to face
		characterForward = character.transform.forward;
		targetForward = target.transform.position - character.transform.position;

		print(target.gameObject.tag);
	}
}
