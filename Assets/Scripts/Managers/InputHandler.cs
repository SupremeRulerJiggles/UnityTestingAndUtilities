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

	// Replay
	Command replay;

	Command nop;

	GameObject player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");

		moveForward = new CommandMove(Vector3.forward, player);
		moveLeft = new CommandMove(Vector3.left, player);
		moveBackward = new CommandMove(Vector3.back, player);
		moveRight = new CommandMove(Vector3.right, player);

		replay = new CommandReplay();

		nop = new CommandNop(player);
	}

	void Update()
	{
		Command input = HandleInput();
		input.Execute(input.target, true);
	}

	public Command HandleInput()
	{
		KeyBindings keyBinds = GameManager.GM.keyBinds;

		if(Input.GetKey(keyBinds.moveForward)){ return moveForward; }
		if(Input.GetKey(keyBinds.moveLeft)){ return moveLeft; }
		if(Input.GetKey(keyBinds.moveBackward)){ return moveBackward; }
		if(Input.GetKey(keyBinds.moveRight)){ return moveRight; }
		else{ return nop; }
	}
}
