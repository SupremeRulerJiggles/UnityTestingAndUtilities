using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pursue : Arrive
{
	[SerializeField] protected float maxPredictionTime;

	void Start () 
	{
		base.Start();
	}

	void FixedUpdate () 
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

	protected override AccelerationOutput GetAcceleration ()
	{
		AccelerationOutput acceleration;
		Vector3 direction;
		float distance;
		float characterSpeed;
		float predictionTime;

		direction = targetPosition - character.transform.position;
		distance = direction.magnitude;

		characterSpeed = characterVelocity.magnitude;

		if(characterSpeed <= distance / maxPredictionTime)
		{
			predictionTime = maxPredictionTime;
		}
		else
		{
			predictionTime = distance / characterSpeed;
		}

		targetPosition += targetVelocity * predictionTime;

		return base.GetAcceleration ();
	}

	protected override void GetInfo()
	{
		targetPosition = target.transform.position;

		characterVelocity = character.GetComponent<Info>().velocity;
		targetVelocity = target.GetComponent<Info>().velocity;
	}
}
