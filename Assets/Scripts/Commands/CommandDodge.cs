using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDodge : Command 
{
	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, true);
	}
}
