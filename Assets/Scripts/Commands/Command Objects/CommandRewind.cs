using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRewind : Command
{
	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, log);

		CommandRewinder rewinder = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().rewinder;

		if(rewinder)
			rewinder.Rewind();
		else
			Debug.Log(MessageText.managerError + "Command Rewinder not found");
	}
}
