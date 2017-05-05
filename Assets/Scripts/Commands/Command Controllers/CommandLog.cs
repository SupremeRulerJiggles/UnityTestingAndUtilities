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
			for(int i = maxSize; i < log.Count; i++)
			{
				log.Remove(log[i]);
			}
		}
	}
}
