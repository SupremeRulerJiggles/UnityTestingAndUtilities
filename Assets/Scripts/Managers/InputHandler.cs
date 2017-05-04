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

	// No input
	Command nop;

	GameObject player;

	bool allowInput = true;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		// Instances of command log commands
		replay = new CommandReplay();
		rewind = new CommandRewind();
		clearLog = new CommandClearLog();

		// Instance of no-op command
		nop = new CommandNop(player);
	}

	void Update()
	{
		// If input is allowed, get the input command and execute the command
		if(allowInput)
		{
			Command moveInput = HandleMovementInput();
			Command logInput = HandleLogInput();

			moveInput.Execute(moveInput.target, true);
			logInput.Execute(logInput.target, true);
		}
	}

	// Get input and return the associated command
	public Command HandleLogInput()
	{
		KeyBindings keyBinds = GameManager.GM.keyBinds;

		if(Input.GetKeyDown(keyBinds.replay)){ return replay; }
		if(Input.GetKeyDown(keyBinds.rewind)){ return rewind; }
		if(Input.GetKeyDown(keyBinds.clearLog)){ return clearLog; }

		return nop;
	}

	public Command HandleMovementInput()
	{
		KeyBindings keyBinds = GameManager.GM.keyBinds;

		Vector3 moveDir = Vector3.zero;

		if(Input.GetKey(keyBinds.moveForward)){ moveDir += Vector3.forward; }
		if(Input.GetKey(keyBinds.moveLeft)){ moveDir += Vector3.left; }
		if(Input.GetKey(keyBinds.moveBackward)){ moveDir += Vector3.back; }
		if(Input.GetKey(keyBinds.moveRight)){ moveDir += Vector3.right; }

		CommandMove newMoveCmd = new CommandMove(moveDir.normalized, player);

		return newMoveCmd;
	}
		
	// Setter for allowing input
	public void AllowInput(bool value)
	{
		allowInput = value;
	}
}
