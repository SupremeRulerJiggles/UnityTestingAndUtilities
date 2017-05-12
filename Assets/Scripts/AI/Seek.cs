using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour 
{
	GameObject character;
	[SerializeField] GameObject target;

	[SerializeField] float maxAcceleration = 5f;
	[SerializeField] float maxSpeed = 5f;

	[SerializeField] bool ignoreY = false;

	Vector3 velocity;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () 
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
	SteeringOutput GetAcceleration()
	{
		SteeringOutput steering = new SteeringOutput();

		steering.linear = target.transform.position - character.transform.position;

		steering.linear.Normalize();
		steering.linear *= maxAcceleration;

		steering.angular = 0;

		return steering;
	}

	// Get the acceleration in 2D
	SteeringOutput GetAcceleration2D()
	{
		SteeringOutput steering = new SteeringOutput();

		steering.linear = target.transform.position - character.transform.position;
		steering.linear = new Vector3(steering.linear.x, 0f, steering.linear.z);

		steering.linear.Normalize();
		steering.linear *= maxAcceleration;

		steering.angular = 0;

		return steering;
	}
}
