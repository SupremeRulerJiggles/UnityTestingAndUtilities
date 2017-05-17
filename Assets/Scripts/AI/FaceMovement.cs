using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMovement : Align
{
	void Start () 
	{
		base.Start();
	}
	
	void FixedUpdate () 
	{
		base.FixedUpdate();
	}

	protected override AccelerationOutput GetAcceleration()
	{
		Vector3 currentVelocity = Vector3.zero;

		currentVelocity = character.GetComponent<Info>().velocity;

		if(currentVelocity.magnitude == 0)
			return null;

		targetForward = currentVelocity;

		return base.GetAcceleration();
	}

	protected override void GetInfo()
	{
		// Direction we are facing
		characterForward = character.transform.forward;
	}
}
