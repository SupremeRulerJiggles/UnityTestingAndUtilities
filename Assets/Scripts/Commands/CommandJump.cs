using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandJump : Command 
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

		obj.SendMessage("Jump");
	}
}
