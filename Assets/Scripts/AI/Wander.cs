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

	float randomTurnNum;

	void Start () 
	{
		target = gameObject;
		centerPos = target.transform.position;

		StartCoroutine("ControlDirection");
	}

	void FixedUpdate () 
	{
		Vector3 vel = (maxSpeed * target.transform.forward.normalized);
		if(randomTurnNum == float.NegativeInfinity)
		{
			target.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
		else
		{
			target.GetComponent<Rigidbody>().velocity = new Vector3(vel.x, 0f, vel.z);
			target.transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles + Vector3.up * maxRotate * randomTurnNum);
		}


	}

	IEnumerator ControlDirection()
	{
		float dis, relDis, relAng;
		float angToCenter, angPosX, angNegX;
		float lastTurnDir = 0f;

		while(true)
		{
			dis = Vector3.Distance(target.transform.position, centerPos);
			relDis = dis / maxRadius;
			relAng = Vector3.Angle(target.transform.position, centerPos - target.transform.position) / 180f;

			angToCenter = Vector3.Angle((center.transform.position - target.transform.position), target.transform.forward);
			angPosX = Vector3.Angle(center.transform.right, target.transform.forward);
			angNegX = Vector3.Angle(-center.transform.right, target.transform.forward);

			// Random range from [-2,2]
			// <0 = turn left
			// 0 = go straight
			// >0 = turn right
			turnDir = Random.Range(-2, 3);

			if(turnDir < 0)
				turnDir = -1;
			else if(turnDir > 0)
				turnDir = 1;

			// If the target is generally facing the center, and it is more than than half the max distance from the center,
			// force the target to go straight
			if(angToCenter < 30f && dis > maxRadius / 2)
			{
				turnDir = 0;
			}

			// Else, if the angle between the target's forward and the positive x axis is less than or equal to the angle between the target's forward and
			// the negative x axis, favor turning right, towards the positive x axis
			else if(angPosX <= angNegX)
			{
				if(turnDir < 0 && Random.Range(0, 3) == 1)
				{
					turnDir = 1;
				}
			}

			// Else, if the previous angle is greater, favor turning left towards the negative x axis
			else
			{
				if(turnDir > 0 && Random.Range(0, 3) == 1)
				{
					turnDir = -1;
				}
			}
				
			// If flipping directions, go straight
			if((lastTurnDir == -1 && turnDir == 1) || (lastTurnDir == 1 && turnDir == -1))
			{
				lastTurnDir = turnDir;
				turnDir = 0;
			}
			// If continuing the same direction, lessen the amount
			else if(lastTurnDir == turnDir && turnDir != 0)
			{
				lastTurnDir = turnDir;
				relDis = relDis / 2f;
			}
			// If going straight twice
			else if(lastTurnDir == turnDir && turnDir == 0)
			{
				lastTurnDir = turnDir;
				turnDir = float.NegativeInfinity;
			}

			if(turnDir == float.NegativeInfinity)
			{
				randomTurnNum = float.NegativeInfinity;
			}
			else
			{
				randomTurnNum = turnDir * relDis * relAng;
			}

			yield return new WaitForSeconds(timeToTurn);
		}

	}
}
