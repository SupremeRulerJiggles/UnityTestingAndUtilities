using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour 
{
	public List<Vector3> PathPoints;

	public List<float> PathPointParams;
	float totalDistance;

	//public Vector3 firstPoint, secondPoint, point;

	void Start () 
	{
		CalculateTotalDistance();
	}

	public Vector3 GetPosition(float param)
	{
		if(PathPointParams.Count == 0)
		{
			return Vector3.zero;
		}
		else if(param > totalDistance)
		{
			return PathPoints[PathPoints.Count-1];
		}

		int previousPoint = 0;

		for(int i = 0; i < PathPointParams.Count; i++)
		{
			if(PathPointParams[i] < param)
			{
				previousPoint = i;
			}
			else
			{
				break;
			}
		}

		float distance = param - PathPointParams[previousPoint];
		Vector3 direction = (PathPoints[previousPoint + 1] - PathPoints[previousPoint]).normalized;

		return PathPoints[previousPoint] + direction * distance;
	}

	public float GetParam(Vector3 position, float lastParam)
	{
		Vector3 pathPosition;

		if(PathPoints.Count == 0)
		{
			return 0f;
		}

		int nearestPoint = 0;

		for(int i = 0; i < PathPoints.Count; i++)
		{
			if((PathPoints[i] - position).magnitude < (PathPoints[nearestPoint] - position).magnitude)
			{
				nearestPoint = i;
			}
		}

		if(PathPoints.Count == 1)
		{
			return 0;
		}
		else if(nearestPoint == 0)
		{
			pathPosition = Math.NearestPointOnLine(position, PathPoints[nearestPoint], PathPoints[nearestPoint + 1]);
			return PathPointParams[nearestPoint] + (pathPosition - PathPoints[nearestPoint]).magnitude;
		}
		else if(nearestPoint == PathPoints.Count-1)
		{
			pathPosition = Math.NearestPointOnLine(position, PathPoints[nearestPoint], PathPoints[nearestPoint - 1]);
			return PathPointParams[nearestPoint - 1] + (pathPosition - PathPoints[nearestPoint - 1]).magnitude;
		}
		else
		{
			Vector3 backPoint = Math.NearestPointOnLine(position, PathPoints[nearestPoint], PathPoints[nearestPoint - 1]);
			Vector3 forwardPoint = Math.NearestPointOnLine(position, PathPoints[nearestPoint], PathPoints[nearestPoint + 1]);

			if((position - forwardPoint).magnitude >= (position - backPoint).magnitude)
			{
				pathPosition = backPoint;
				return PathPointParams[nearestPoint - 1] + (pathPosition - PathPoints[nearestPoint - 1]).magnitude;
			}
			else
			{
				pathPosition = forwardPoint;
				return PathPointParams[nearestPoint] + (pathPosition - PathPoints[nearestPoint]).magnitude;
			}
		}
	}

	void CalculateTotalDistance()
	{
		float distance = 0f;

		for(int i = 0; i < PathPoints.Count - 1; i++)
		{
			PathPointParams.Add(distance);
			distance += (PathPoints[i + 1] - PathPoints[i]).magnitude;
		}

		totalDistance = distance;
	}

/*	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		for(int i = 0; i < PathPoints.Count - 1; i++)
		{
			Gizmos.DrawLine(PathPoints[i], PathPoints[i+1]);
		}

		print(GetParam(GameObject.FindGameObjectWithTag("Player").transform.position, 0f));
		Gizmos.color = Color.black;
		Gizmos.DrawLine(GameObject.FindGameObjectWithTag("Player").transform.position, 
			GetPosition(GetParam(GameObject.FindGameObjectWithTag("Player").transform.position, 0f)));
	}*/
}
