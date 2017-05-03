using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyBindings))]
[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(CommandLog))]
public class GameManager : MonoBehaviour 
{
	public static GameManager GM;

	public KeyBindings keyBinds;
	public InputHandler inputHandler;
	public CommandLog cmdLog;

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
	}

	void Start () 
	{
		
	}
	
	void Update () 
	{

	}
}
