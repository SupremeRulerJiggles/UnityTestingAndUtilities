using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandAttack : Command  
{
	void Start () 
	{

	}

	void Update () 
	{

	}

	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, true);
	}
}
