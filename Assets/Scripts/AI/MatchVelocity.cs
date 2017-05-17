using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchVelocity : MonoBehaviour 
{
	protected GameObject character;
	[SerializeField] protected GameObject target;

	[SerializeField] protected float maxAcceleration = 10f;
	[SerializeField] protected float timeToTarget = .1f;

	protected Vector3 characterVelocity;
	protected Vector3 targetVelocity;

	protected Vector3 velocity;

	protected void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	protected void FixedUpdate () 
	{
		GetInfo();

		AccelerationOutput acceleration = GetAcceleration();

		velocity += acceleration.linear * Time.fixedDeltaTime;

		character.transform.position += velocity * Time.fixedDeltaTime;
	}

	protected virtual AccelerationOutput GetAcceleration()
	{
		AccelerationOutput acceleration = new AccelerationOutput();

		acceleration.linear = targetVelocity - characterVelocity;
		acceleration.linear /= timeToTarget;

		if(acceleration.linear.magnitude > maxAcceleration)
		{
			acceleration.linear.Normalize();
			acceleration.linear *= maxAcceleration;
		}

		acceleration.angular = 0f;

		return acceleration;
	}

	protected virtual void GetInfo()
	{
		characterVelocity = character.GetComponent<Info>().velocity;
		targetVelocity = target.GetComponent<Info>().velocity;
	}
}
