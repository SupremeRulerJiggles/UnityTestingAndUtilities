using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math
{
	public static float randomBinomial()
	{
		return Random.Range(0f, 1f) - Random.Range(0f, 1f);
	}
}
