using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandClearLog : Command
{
	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, log);

		cmdLog.ClearLog();
	}
}
