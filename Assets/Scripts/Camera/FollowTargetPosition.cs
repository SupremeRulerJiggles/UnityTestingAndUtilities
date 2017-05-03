using UnityEngine;
using System.Collections;

public class FollowTargetPosition : MonoBehaviour 
{
	public GameObject trackTarget;
	public float smoothTime;
	public float climbSmoothTime;
	public float rampUpSpeedAfterClimb;
	public bool follow{get; set;}
	float ogSmoothTime;

	float returnToOGSpeedStartTime = 0f;

	public Vector3 trackOffset;
	Vector3 currentVelocity;

	void Awake()
	{
		if(!trackTarget)
		{
			trackTarget = GameObject.FindGameObjectWithTag("Player");
		}

		MoveToTargetPosition();

		follow = true;

		ogSmoothTime = smoothTime;
	}

	void FixedUpdate()
	{
		if(!follow)
			return;

		if(trackOffset.magnitude == 0 && smoothTime != ogSmoothTime)
		{
			if(Vector3.Distance(transform.position, trackTarget.transform.position) < .1f)
			{
				SetToClimbSpeed(false);
			}
			else
			{
				smoothTime -= Time.deltaTime * rampUpSpeedAfterClimb;
			}

			if(returnToOGSpeedStartTime != 0f)
			{
				returnToOGSpeedStartTime = Time.time;
			}
		}
			
		transform.position = Vector3.SmoothDamp(transform.position, trackTarget.transform.position + trackOffset, ref currentVelocity, smoothTime);
	}

	public void SetOffset(Vector3 newPosition, Vector3 currentPosition)
	{
		trackOffset = currentPosition - newPosition;
		smoothTime = climbSmoothTime;
	}

	public void SetToClimbSpeed(bool value)
	{
		if(value)
		{
			smoothTime = climbSmoothTime;
		}
		else
		{
			smoothTime = ogSmoothTime;
			returnToOGSpeedStartTime = 0f;
		}
			
	}

	public void MoveToTargetPosition()
	{
		if(trackTarget)
			transform.position = trackTarget.transform.position;
		else
			transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
}
