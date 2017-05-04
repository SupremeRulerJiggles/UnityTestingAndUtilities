using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandReplay : Command
{
	public override void Execute(GameObject obj, bool log)
	{
		base.Execute(obj, log);

		CommandReplayer replayer;

		replayer = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().replayer;

		if(replayer)
			replayer.Replay();
		else
			Debug.Log(MessageText.managerError + "Command Replayer not found");
	}
}
