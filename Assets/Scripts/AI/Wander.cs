using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Align
{
	[SerializeField] float wanderOffset = 2f;
	[SerializeField] float wanderRadius = 1f;

	[SerializeField] float wanderRate =1f;

	[SerializeField] float maxSpeed = 1f;
	[SerializeField] float maxAcceleration = 10f;

	Vector3 velocity;

	protected void Start()
	{
		base.Start();
	}

	protected void FixedUpdate()
	{
		GetInfo();

		AccelerationOutput acceleration = GetAcceleration();

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

			velocity += acceleration.linear * Time.fixedDeltaTime;

			if(velocity.magnitude > maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed;
			}

			transform.position += velocity * Time.fixedDeltaTime;
		}
		else
		{
			rotation = 0f;
		}
			

	}

	protected override AccelerationOutput GetAcceleration()
	{
		AccelerationOutput acceleration;

		float wanderOrientation;

		Vector3 lookPosition;

		wanderOrientation = characterOrientation;
		wanderOrientation += Math.RandomBinomial() * wanderRate;
		targetOrientation = wanderOrientation + characterOrientation;

		lookPosition = character.transform.position + wanderOffset * character.transform.forward;
		lookPosition += Math.OrientationVector(targetOrientation) * wanderRadius;

		targetForward = lookPosition - character.transform.position;
		targetOrientation = Mathf.Atan2(targetForward.x, targetForward.z);

		acceleration = base.GetAcceleration();

		if(acceleration == null)
		{
			return null;
		}

		acceleration.linear = maxAcceleration * character.transform.forward;

		return acceleration;
	}

	protected override void GetInfo()
	{
		characterOrientation = Mathf.Atan2(characterForward.x, characterForward.z);
		characterForward = character.transform.forward;
	}
}
