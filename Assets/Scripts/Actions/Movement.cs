using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour 
{
	public float MaxSpeed = 1f;
	public float AccelSpeed = 1f;

	CharacterController control;

	void Start()
	{
		control = GetComponent<CharacterController>();
	}

	public void Move(Vector3 dir)
	{
		try
		{ 
			Vector3 newVel = GetAcceleratedSpeed(dir);

			//control.Move(dir * Time.deltaTime * MaxSpeed);
			GetComponent<Rigidbody>().velocity = newVel;
		}
		catch{ Debug.Log(MessageText.managerError + "Tried to move an object without a rigidBody", gameObject); }
	}

	public void Turn(Vector3 dir)
	{
		StopCoroutine("TurnToDirection");
		StartCoroutine("TurnToDirection", dir);
	}

	IEnumerator TurnToDirection(Vector3 dir)
	{
		Vector3 vel = Vector3.zero;

		while(Vector3.Angle(transform.forward, dir) > 1f)
		{
			transform.forward = Vector3.SmoothDamp(transform.forward, dir, ref vel, .1f);

			yield return new WaitForFixedUpdate();
		}
	}

	Vector3 GetAcceleratedSpeed(Vector3 dir)
	{
		return Vector3.Lerp(control.velocity, dir * MaxSpeed, AccelSpeed * Time.deltaTime);
	}
}
