using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour 
{
	GameObject character;
	[SerializeField] GameObject target;

	[SerializeField] float maxAcceleration = 5f;
	[SerializeField] float maxSpeed = 5f;

	[SerializeField] float targetRadius = .1f;
	[SerializeField] float slowRadius = 5f;

	[SerializeField] float timeToTarget = .1f;

	Vector3 velocity;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () 
	{
		SteeringOutput steering = GetSteering();

		if(steering != null)
		{
			velocity += steering.linear * Time.fixedDeltaTime;

			if(velocity.magnitude > maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed;
			}
				
			transform.position += velocity * Time.fixedDeltaTime;
		}
		else
		{
			velocity = Vector3.zero;
		}
	}

	// Returns a SteeringOutput object who's linear vector is the acceleration amount for this frame
	SteeringOutput GetSteering()
	{
		SteeringOutput steering = new SteeringOutput();
		Vector3 direction = Vector3.zero;
		Vector3 targetVelocity = Vector3.zero;

		// Direction and distance from character to target
		direction = target.transform.position - character.transform.position;
		float distance = direction.magnitude;

		// Stores the desired movement speed of the object
		float targetSpeed = 0f;

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
		targetVelocity = direction;
		targetVelocity.Normalize();
		targetVelocity *= targetSpeed;

		// Acceleration is the difference in target and current velocities
		// Then divide by the time factor to scale it up or down
		steering.linear = targetVelocity - velocity;
		steering.linear /= timeToTarget;

		// If the acceleration amount is greater than the max acceleration, normalize it
		if(steering.linear.magnitude > maxAcceleration)
		{
			steering.linear.Normalize();
			steering.linear *= maxAcceleration;
		}

		steering.angular = 0;

		return steering;
	}
}