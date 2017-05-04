using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyBindings))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(CommandLog))]
[RequireComponent(typeof(CommandReplayer))]
[RequireComponent(typeof(CommandRewinder))]
public class GameManager : MonoBehaviour 
{
	public static GameManager GM;

	public KeyBindings keyBinds;
	public InputHandler inputHandler;
	public CommandLog cmdLog;
	public CommandReplayer replayer;
	public CommandRewinder rewinder;

	void Awake()
	{
		// Singleton Pattern
		if(GM == null)
		{
			DontDestroyOnLoad(GM);
			GM = this;
		}
		else if(GM != this)
		{
			Destroy(gameObject);
		}

		// References
		keyBinds = GetComponent<KeyBindings>();
		inputHandler = GetComponent<InputHandler>();
		cmdLog = GetComponent<CommandLog>();
		replayer = GetComponent<CommandReplayer>();
		rewinder = GetComponent<CommandRewinder>();
	}
}
