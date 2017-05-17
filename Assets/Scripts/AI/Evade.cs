using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee
{
	[SerializeField] protected float maxPredictionTime;

	Vector3 characterVelocity;
	Vector3 targetVelocity;

	void Start () 
	{
		base.Start();
	}

	void FixedUpdate () 
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

	protected override AccelerationOutput GetAcceleration()
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

		if((target.transform.position - character.transform.position).magnitude < targetVelocity.magnitude * predictionTime)
		{
			targetPosition = target.transform.position;
		}

		return base.GetAcceleration ();
	}
		
	protected override AccelerationOutput GetAcceleration2D()
	{
		AccelerationOutput acceleration;
		Vector3 direction;
		float distance;
		float characterSpeed;
		float predictionTime;

		direction = targetPosition - character.transform.position;
		direction = new Vector3(direction.x, 0f, direction.z);
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

		if((target.transform.position - character.transform.position).magnitude < targetVelocity.magnitude * predictionTime)
		{
			targetPosition = target.transform.position;
		}

		return base.GetAcceleration2D ();
	}

	protected override void GetInfo()
	{
		targetPosition = target.transform.position;

		characterVelocity = character.GetComponent<Info>().velocity;
		targetVelocity = target.GetComponent<Info>().velocity;
	}
}
