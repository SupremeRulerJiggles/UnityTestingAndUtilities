using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLog : MonoBehaviour
{
	List<Command> buffer = new List<Command>();
	List<Command> log = new List<Command>();
	int maxSize = 10000;

	bool allowCommands = true;

	void Update()
	{
		LimitLogSize();
		UpdateLog();
	}

	public void Replay()
	{
		StartCoroutine("ReplayCommands");
	}

	IEnumerator ReplayCommands()
	{
		allowCommands = false;

		Command[] temp = new Command[log.Count];
		log.CopyTo(temp);

		int nopCount = 0;

		for(int i = 0; i < temp.Length; i++)
		{
			if(temp[i] is CommandNop)
			{
				nopCount++;
			}
			else
			{
				break;
			}
		}

		for(int i = nopCount; i < temp.Length; i++)
		{
			temp[i].Execute(temp[i].target, false);

			yield return new WaitForEndOfFrame();
		}
				
		log.Clear();

		allowCommands = true;
	}

	public void AddCommand(Command com)
	{
		buffer.Add(com);
	}

	public void UpdateLog()
	{
		if(allowCommands)
		{
			foreach(Command cmd in buffer)
			{
				log.Add(cmd);
			}

			buffer.Clear();
		}
	}

	public List<Command> GetCurrentLog()
	{
		return log;
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
