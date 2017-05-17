using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour 
{
	protected GameObject character;
	[SerializeField] protected GameObject target;

	[SerializeField] protected float maxAcceleration = 5f;
	[SerializeField] protected float maxSpeed = 5f;

	[SerializeField] protected bool ignoreY = false;

	protected Vector3 velocity;

	protected void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	protected void FixedUpdate () 
	{
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
		AccelerationOutput acceleration = new AccelerationOutput();

		acceleration.linear = target.transform.position - character.transform.position;

		acceleration.linear.Normalize();
		acceleration.linear *= maxAcceleration;

		acceleration.angular = 0;

		return acceleration;
	}

	// Get the acceleration in 2D
	protected virtual AccelerationOutput GetAcceleration2D()
	{
		AccelerationOutput acceleration = new AccelerationOutput();

		acceleration.linear = target.transform.position - character.transform.position;
		acceleration.linear = new Vector3(acceleration.linear.x, 0f, acceleration.linear.z);

		acceleration.linear.Normalize();
		acceleration.linear *= maxAcceleration;

		acceleration.angular = 0;

		return acceleration;
	}
}
