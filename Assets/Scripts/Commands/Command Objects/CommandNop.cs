using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandNop : Command
{
	public CommandNop(GameObject obj)
	{
		target = obj;
	}

	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, log);
	}

	public override void Undo(GameObject obj)
	{
		base.Undo(obj);
	}
}
