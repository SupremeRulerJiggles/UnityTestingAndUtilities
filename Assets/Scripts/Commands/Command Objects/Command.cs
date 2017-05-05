using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Command
{
	protected CommandLog cmdLog;
	public GameObject target;

	public virtual void Execute(GameObject obj, bool log)
	{ 
		target = obj;

		if(cmdLog)
			LogCommand(); 
			
		else
		{
			if(!cmdLog)
			{
				try{ cmdLog = GameManager.GM.cmdLog; }
				catch { Debug.Log(MessageText.managerError + "The Command Log component could not be found on the Game Controller"); }
			}

			LogCommand();
		}
	}

	public virtual void Undo(GameObject obj)
	{
		target = obj;
	}

	protected virtual void LogCommand()
	{ 
		cmdLog.AddCommand(this); 
	}
}

