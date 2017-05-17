using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : Align
{
	public GameObject faceTarget;

	protected void Start () 
	{
		if(!faceTarget)
			faceTarget = GameObject.FindGameObjectWithTag("Player");

		target = faceTarget;

		base.Start();
	}

	protected void FixedUpdate () 
	{
		base.FixedUpdate();
	}

	protected override AccelerationOutput GetAcceleration()
	{
		Vector3 direction;

		direction = faceTarget.transform.position - character.transform.position;

		if(direction.magnitude == 0)
			return null;

		target = faceTarget;

		return base.GetAcceleration();
	}
}
