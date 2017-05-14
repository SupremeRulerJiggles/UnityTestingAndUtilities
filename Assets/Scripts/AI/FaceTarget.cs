using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : Align
{
	public GameObject faceTarget;

	void Start () 
	{
		target = faceTarget;

		base.Start();
	}

	void FixedUpdate () 
	{
		base.FixedUpdate();
	}

	protected override SteeringOutput GetAcceleration()
	{
		Vector3 direction;

		direction = faceTarget.transform.position - character.transform.position;

		if(direction.magnitude == 0)
			return null;

		target = faceTarget;

		return base.GetAcceleration();
	}
}
