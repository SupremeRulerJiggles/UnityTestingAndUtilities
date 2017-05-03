using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour 
{
	// Movement
	Command moveForward;
	Command moveLeft;
	Command moveBackward;
	Command moveRight;

	// Command Log
	Command replay;
	Command rewind;
	Command clearLog;

	Command nop;

	GameObject player;

	bool allowInput = true;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		moveForward = new CommandMove(Vector3.forward, player);
		moveLeft = new CommandMove(Vector3.left, player);
		moveBackward = new CommandMove(Vector3.back, player);
		moveRight = new CommandMove(Vector3.right, player);

		replay = new CommandReplay();
		rewind = new CommandRewind();
		clearLog = new CommandClearLog();

		nop = new CommandNop(player);
	}

	void Update()
	{
		if(allowInput)
		{
			Command input = HandleInput();
			input.Execute(input.target, true);
		}
	}

	public Command HandleInput()
	{
		KeyBindings keyBinds = GameManager.GM.keyBinds;

		if(Input.GetKey(keyBinds.moveForward)){ return moveForward; }
		if(Input.GetKey(keyBinds.moveLeft)){ return moveLeft; }
		if(Input.GetKey(keyBinds.moveBackward)){ return moveBackward; }
		if(Input.GetKey(keyBinds.moveRight)){ return moveRight; }
		if(Input.GetKeyDown(keyBinds.replay)){ return replay; }
		if(Input.GetKeyDown(keyBinds.rewind)){ return rewind; }
		if(Input.GetKeyDown(keyBinds.clearLog)){ return clearLog; }

		return nop;
	}

	public void AllowInput(bool value)
	{
		allowInput = value;
	}
}
