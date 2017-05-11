using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command 
{
	public Vector3 moveDir;
	public Vector3 pos;

	public CommandMove(Vector3 direction, GameObject obj)
	{
		moveDir = direction;
		target = obj;
	}

	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, log);

		try
		{ 
			obj.GetComponent<Movement>().Move(moveDir); 
			obj.GetComponent<Movement>().Turn(moveDir); 
		}
		catch{ Debug.Log(MessageText.cmdError + "Tried to send move command to an object without the movement script!", obj); }

		pos = obj.transform.position;
	}

	public override void Undo(GameObject obj)
	{
		base.Undo(obj);

		try
		{
			obj.GetComponent<Movement>().Move(-moveDir); 
			obj.GetComponent<Movement>().Turn(moveDir); 
		}
		catch{ Debug.Log(MessageText.cmdError + "Tried to send move command to an object without the movement script!", obj); }
	}
}
