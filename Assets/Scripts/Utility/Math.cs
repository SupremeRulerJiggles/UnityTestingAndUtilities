using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math
{
	public static float RandomBinomial()
	{
		return Random.Range(0f, 1f) - Random.Range(0f, 1f);
	}

	public static float MapToRange(float num, float oldMin, float oldMax, float newMin, float newMax)
	{
		return (num - oldMin) / (oldMax - oldMin) * (newMax - newMin) + newMin;
	}

	// Maps a given float to a number between (-Pi, Pi]
	// Assumes that the number given is based in the range from [0, 360)
	public static float MapFromDegreesToPi(float num)
	{
		float mod = num % 360;
		float multiple = (num - mod) / 360;
		float sign = num / Mathf.Abs(num);

		if(Mathf.Abs(mod) > 180)
		{
			return sign * Math.MapToRange(
				(Mathf.Abs(num)) - (sign * multiple * 360), 
				180, 360, 
				-Mathf.PI, 0
			);
		}
		else
		{
			return Math.MapToRange(num, 0, 180, 0, Mathf.PI) - (2*multiple*Mathf.PI);
		}
	}

	// Maps a given float to a number between [0, 360)
	// Assumes that the number given is based in the range from (-Pi, Pi]
	public static float MapFromPiToDegrees(float num)
	{
		float sign = num / Mathf.Abs(num);

		if(sign > 0)
		{
			return MapToRange(num, 0, Mathf.PI, 0, 180);
		}
		else
		{
			return MapToRange(num, -Mathf.PI, 0, 180, 360);
		}
	}
}
