using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLog : MonoBehaviour
{
	List<Command> buffer = new List<Command>();
	List<Command> log = new List<Command>();
	int maxSize = 10000;

	bool allowCommands = true;

	InputHandler inputHandler;

	void Update()
	{
		LimitLogSize();
		UpdateLog();
	}

	public void Replay()
	{
		StartCoroutine("ReplayCommands");
	}

	public void Rewind()
	{
		StartCoroutine("RewindCommands");
	}

	public void AddCommand(Command com)
	{
		buffer.Add(com);
	}

	public List<Command> GetCurrentLog()
	{
		return log;
	}

	public void ClearLog()
	{
		log.Clear();
	}

	IEnumerator ReplayCommands()
	{
		try{ inputHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inputHandler; }
		catch { Debug.Log(MessageText.managerError + "The Input Log component could not be found on the Game Controller"); yield break; }

		inputHandler.AllowInput(false);

		Command[] temp = new Command[log.Count];
		log.CopyTo(temp);

		int startCount = 0;
		int endCount = temp.Length - 1;

		for(int i = 0; i < temp.Length; i++)
		{
			if(temp[i] is CommandNop)
			{
				startCount++;
			}
			else
			{
				break;
			}
		}

		for(int i = temp.Length - 1; i >= 0; i--)
		{
			if(temp[i] is CommandNop)
			{
				endCount--;
			}
			else
			{
				break;
			}
		}

		for(int i = startCount; i <= endCount; i++)
		{
			temp[i].Execute(temp[i].target, false);

			yield return new WaitForEndOfFrame();
		}
				
		log.Clear();

		inputHandler.AllowInput(true);
	}

	IEnumerator RewindCommands()
	{
		try{ inputHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inputHandler; }
		catch { Debug.Log(MessageText.managerError + "The Input Log component could not be found on the Game Controller"); yield break; }

		inputHandler.AllowInput(false);

		Command[] temp = new Command[log.Count];
		log.CopyTo(temp);

		int startCount = temp.Length - 1;
		int endCount = 0;

		for(int i = temp.Length - 1; i >= 0; i--)
		{
			if(temp[i] is CommandNop)
			{
				startCount--;
			}
			else
			{
				break;
			}
		}

		for(int i = 0; i < temp.Length; i++)
		{
			if(temp[i] is CommandNop)
			{
				endCount++;
			}
			else
			{
				break;
			}
		}

		for(int i = startCount; i >= endCount; i--)
		{
			temp[i].Undo(temp[i].target);

			yield return new WaitForEndOfFrame();
		}

		log.Clear();

		inputHandler.AllowInput(true);
	}

	void UpdateLog()
	{
		if(allowCommands)
		{
			foreach(Command cmd in buffer)
			{
				log.Add(cmd);
			}
		}

		buffer.Clear();
	}
		
	void LimitLogSize()
	{
		if(log.Count > maxSize)
		{
			for(int i = maxSize; i <= log.Count; i++)
			{
				log.Remove(log[i]);
			}
		}
	}
}
