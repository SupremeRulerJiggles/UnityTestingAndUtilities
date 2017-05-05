using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandReplayer : MonoBehaviour
{
	InputHandler inputHandler;
	CommandLog cmdLog;

	void Start()
	{
		try
		{
			inputHandler = GameManager.GM.inputHandler;
			cmdLog = GameManager.GM.cmdLog;
		}
		catch{ Debug.Log(MessageText.managerError + "Game Manager could not be found.", this); }
	}

	public void Replay()
	{
		StartCoroutine("ReplayMovementCommands");
	}

	IEnumerator ReplayMovementCommands()
	{
		List<Command> log;

		if(!inputHandler || !cmdLog)
		{
			Debug.Log(MessageText.managerError + "Input Handler or Command Log missing.", this);
			yield break;
		}

		// Don't allow any more commands to be entered
		inputHandler.AllowInput(false);

		// Get the current command log and copy it to an array
		log = cmdLog.GetCurrentLog();
		Command[] temp = new Command[log.Count];
		log.CopyTo(temp);

		// The start index will be the first array index (0), and he last index will be the final array index (array length - 1)
		int startCount = 0;
		int endCount = temp.Length - 1;

		CommandMove cmd;

		// Set the start index to ignore all zero vector move commands before the first nonzero command
		for(int i = 0; i < temp.Length; i++)
		{
			if(!(temp[i] is CommandMove))
			{
				startCount++;
				continue;
			}
				
			cmd = (CommandMove)temp[i];

			if(cmd.moveDir == Vector3.zero)
				startCount++;
			else
				break;
		}

		// Set the end index to ignore all zero vector move commands after the last nonzero command
		for(int i = temp.Length - 1; i >= 0; i--)
		{
			if(!(temp[i] is CommandMove))
			{
				endCount--;
				continue;
			}

			cmd = (CommandMove)temp[i];

			if(cmd.moveDir == Vector3.zero)
				endCount--;
			else 
				break;
				
		}

		// Using the adjusted start and end indices, execute all movement commands in the log
		for(int i = startCount; i <= endCount; i++)
		{
			if(!(temp[i] is CommandMove))
				continue;

			cmd = (CommandMove)temp[i];

			temp[i].Execute(temp[i].target, false);

			yield return new WaitForEndOfFrame();
		}

		// Clear the command log now that the replay is over
		cmdLog.ClearLog();

		// Allow commands to be issued again
		inputHandler.AllowInput(true);
	}
}
