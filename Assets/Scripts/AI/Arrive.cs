using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour 
{
	GameObject character;
	GameObject target;

	Info characterInfo;

	float maxAcceleration = 100f;
	float maxSpeed = 100f;

	float targetRadius = 1f;
	float slowRadius = 100f;

	float timeToTarget = .6f;

	Vector3 velocity;

	void Start () 
	{
		character = gameObject;
		characterInfo = GetComponent<Info>();
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () 
	{
		print(transform.position);

		SteeringOutput steering = GetSteering2D();
		if(steering != null)
		{
			Vector3 newVelocity = GetSteering2D().linear * Time.fixedDeltaTime;
			velocity += newVelocity;

			if(velocity.magnitude / Time.fixedDeltaTime > maxSpeed)
			{
				velocity = velocity.normalized * maxSpeed * Time.fixedDeltaTime;
			}

			transform.position += velocity;
		}
		else
		{
			velocity = Vector3.zero;
		}
	}

	// Returns a SteeringOutput object who's linear vector is the acceleration amount for this frame
	SteeringOutput GetSteering2D()
	{
		SteeringOutput steering = new SteeringOutput();

		// Direction from character to target
		Vector3 direction = target.transform.position - character.transform.position;

		// Distance from character to target
		float distance = direction.magnitude;

		// If the distance is within the stopping radius, return null
		if(distance < targetRadius)
		{
			return null;
		}
		// If the distance is outside the slow down radius, move at max speed
		else if(distance > slowRadius)
		{
			steering.linear = direction.normalized * maxSpeed;
		}
		// If the distance is within the slow down radius, calculate the speed based on distance from the target
		else
		{
			float targetSpeed = maxSpeed * distance / slowRadius;

			Vector3 targetVelocity = direction.normalized * targetSpeed;
			Vector3 currentVelocity2D = new Vector3(characterInfo.velocity.x, 0f, characterInfo.velocity.z);

			steering.linear = targetVelocity - currentVelocity2D;
			steering.linear /= timeToTarget;

			if(steering.linear.magnitude > maxAcceleration)
			{
				steering.linear.Normalize();
				steering.linear *= maxAcceleration;
			}
		}

		steering.angular = 0;

		return steering;
	}
}
