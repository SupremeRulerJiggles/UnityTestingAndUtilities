using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Command
{
	protected CommandLog log;
	public GameObject target;

	public Command()
	{
/*		try{ log = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().cmdLog; }
		catch { Debug.Log(MessageText.managerError + "The Command Log component could not be found on the Game Controller"); }*/
	}

	public virtual void Execute(GameObject obj, bool log)
	{ 
		target = obj;



		if(log)
			LogCommand(); 
		else
		{
			try{ log = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().cmdLog; }
			catch { Debug.Log(MessageText.managerError + "The Command Log component could not be found on the Game Controller"); }

			LogCommand();
		}
	}

	protected virtual void LogCommand()
	{ 
		log.AddCommand(this); 
	}
}

