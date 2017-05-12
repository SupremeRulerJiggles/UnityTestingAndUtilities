using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour 
{
	GameObject character;
	GameObject target;

	float maxAcceleration = 4f;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate () 
	{
		Vector3 newVelocity = GetSteering2D().linear * Time.deltaTime;

		transform.position += newVelocity;
	}

	SteeringOutput GetSteering2D()
	{
		SteeringOutput steering = new SteeringOutput();

		steering.linear = character.transform.position - target.transform.position;
		steering.linear = new Vector3(steering.linear.x, 0f, steering.linear.z);

		steering.linear.Normalize();
		steering.linear *= maxAcceleration;

		steering.angular = 0;

		return steering;
	}
}
