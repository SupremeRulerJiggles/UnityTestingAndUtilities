using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour 
{
	public GameObject center;
	GameObject target;
	float maxSpeed = 2f;
	float maxRotate = 2f;

	Vector3 centerPos;
	float maxRadius = 10f;

	float timeToTurn = 1f;
	float timeWasTurned = 0f;
	float turnDir = 1f;

	void Start () 
	{
		target = gameObject;
		centerPos = target.transform.position;
	}

	void FixedUpdate () 
	{
		float rand = RandomTurnAroundPoint();
			
		Vector3 vel = (maxSpeed * target.transform.forward.normalized);
		if(rand == -2)
		{
			vel = Vector3.zero;
			rand = 0;
		}
			

		target.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, 0f, vel.z);
		target.transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles + Vector3.up * maxRotate * rand);
	}

	float RandomTurnAroundPoint()
	{
		float dis = Vector3.Distance(target.transform.position, centerPos);
		float relDis = dis / maxRadius;
		float relAng = Vector3.Angle(target.transform.position, centerPos - target.transform.position) / 180f;

		if(timeWasTurned + timeToTurn < Time.time)
		{
			timeWasTurned = Time.time;
			turnDir = Random.Range(-1, 2);
			if(turnDir == 0)
			{
				turnDir = Random.Range(-1, 2);
				if(turnDir == 0 && Random.Range(-1, 2) == 0)
					return -2;
			}
				
		}

		if(Vector3.Angle(center.transform.right, target.transform.forward) <= Vector3.Angle(-center.transform.right, target.transform.forward))
		{
			if(turnDir == -1 && Random.Range(0, 2) == 1)
			{
				turnDir = 1;
			}
		}
		else
		{
			if(turnDir == 1 && Random.Range(0, 2) == 1)
			{
				turnDir = -1;
			}
		}

		float rand = turnDir * relDis * relAng;

		return rand;
	}
}
