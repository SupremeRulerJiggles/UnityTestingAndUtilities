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
		base.Execute(obj, true);

		try
		{ obj.GetComponent<Movement>().Move(Vector3.zero); }
		catch{ Debug.Log(MessageText.cmdError + "Tried to send move command to an object without the movement script!", obj); }
	}
}
