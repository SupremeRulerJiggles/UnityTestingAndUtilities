using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour 
{
	GameObject character;
	GameObject target;

	float maxAcceleration = .01f;
	float maxSpeed = 1f;

	Vector3 velocity;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () 
	{
		Vector3 newVelocity = GetSteering2D().linear * Time.deltaTime;
		velocity += newVelocity;

		print(velocity);

		if(velocity.magnitude > maxSpeed)
			velocity = velocity.normalized * maxSpeed;
			

		transform.position += velocity;
	}

	SteeringOutput GetSteering2D()
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
