using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour 
{
	protected GameObject character;
	[SerializeField] protected GameObject target;

	[SerializeField] protected float maxAcceleration = 5f;
	[SerializeField] protected float maxSpeed = 5f;

	[SerializeField] protected bool ignoreY = true;

	protected Vector3 velocity;

	protected Vector3 targetPosition;

	protected void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	protected void FixedUpdate () 
	{
		GetInfo();

		if(!ignoreY)
		{
			velocity += GetAcceleration().linear * Time.fixedDeltaTime;
		}
		else
		{
			velocity += GetAcceleration2D().linear * Time.fixedDeltaTime;
		}

		if(velocity.magnitude > maxSpeed)
		{
			velocity = velocity.normalized * maxSpeed;
		}

		transform.position += velocity * Time.fixedDeltaTime;
	}

	// Get the acceleration in 3D
	protected virtual AccelerationOutput GetAcceleration()
	{
		AccelerationOutput steering = new AccelerationOutput();

		steering.linear = character.transform.position - targetPosition;

		steering.linear.Normalize();
		steering.linear *= maxAcceleration;

		steering.angular = 0;

		return steering;
	}

	// Get the acceleration in 2D
	protected virtual AccelerationOutput GetAcceleration2D()
	{
		AccelerationOutput steering = new AccelerationOutput();

		steering.linear = character.transform.position - targetPosition;
		steering.linear = new Vector3(steering.linear.x, 0f, steering.linear.z);

		steering.linear.Normalize();
		steering.linear *= maxAcceleration;

		steering.angular = 0;

		return steering;
	}

	protected virtual void GetInfo()
	{
		targetPosition = target.transform.position;
	}
}
