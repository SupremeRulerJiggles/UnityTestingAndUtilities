using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MatchVelocity
{
	[SerializeField] protected float maxSpeed = 5f;

	[SerializeField] protected float targetRadius = .1f;
	[SerializeField] protected float slowRadius = 5f;

	protected Vector3 targetPosition;

	protected void Start () 
	{
		base.Start();
	}

	protected void FixedUpdate () 
	{
		GetInfo();

		AccelerationOutput acceleration = GetAcceleration();

		if(acceleration != null)
		{
			velocity += acceleration.linear * Time.fixedDeltaTime;

			if(velocity.magnitude > maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed;
			}
				
			character.transform.position += velocity * Time.fixedDeltaTime;
		}
		else
		{
			velocity = Vector3.zero;
		}
	}

	// Returns a SteeringOutput object who's linear vector is the acceleration amount for this frame
	protected override AccelerationOutput GetAcceleration()
	{
		AccelerationOutput acceleration;
		Vector3 direction;
		Vector3 desiredVelocity;
		float distance;
		float targetSpeed;

		acceleration = new AccelerationOutput();

		// Direction and distance from character to target
		direction = targetPosition - character.transform.position;
		distance = direction.magnitude;

		// Stores the desired movement speed of the object
		targetSpeed = 0f;

		// If the distance is within the stopping radius, return null
		if(distance < targetRadius)
		{
			return null;
		}
		// If the distance is outside the slow down radius, move at max speed
		else if(distance > slowRadius)
		{
			targetSpeed = maxSpeed;
		}
		// If the distance is within the slow down radius, calculate the speed based on distance from the target
		else
		{
			targetSpeed = maxSpeed * distance / slowRadius;
		}

		// Normalize the direction we want to move in and multiply it by the target speed
		desiredVelocity = direction;
		desiredVelocity.Normalize();
		desiredVelocity *= targetSpeed;

		characterVelocity = velocity;
		targetVelocity = desiredVelocity;

		// Now that the character and target velocities have been calculated, simply call
		// the function in MatchVelocity
		return base.GetAcceleration();
	}

	protected override void GetInfo()
	{
		targetPosition = target.transform.position;
	}
}