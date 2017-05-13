using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour 
{
	GameObject character;
	[SerializeField] GameObject target;

	[SerializeField] float maxAngularAcceleration;
	[SerializeField] float maxRotation;

	[SerializeField] float targetRadius;
	[SerializeField] float slowRadius;

	[SerializeField] float timeToTarget = .1f;

	public float modNum = 50;

	void Start () 
	{
		character = gameObject;
		if(!target)
			target = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		print(Math.MapToRangePI(-655));
		print(Math.MapToRange(Math.MapToRangePI(-655), -Mathf.PI, Mathf.PI, 0, 10));
	}

	SteeringOutput GetAcceleration()
	{
		SteeringOutput acceleration = new SteeringOutput();

		Vector3 targetOrientation = target.transform.forward;
		Vector3 characterOrientation = character.transform.forward;

		Vector3 rotation = targetOrientation - characterOrientation;

		return null;
	}
}
