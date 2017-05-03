using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindings : MonoBehaviour
{
	public static KeyBindings bindings;

	public KeyCode moveForward;
	public KeyCode moveLeft;
	public KeyCode moveRight;
	public KeyCode moveBackward;

	public KeyCode replay;

	void Awake()
	{
		if(moveForward == KeyCode.None){ moveForward = KeyCode.W; }
		if(moveLeft == KeyCode.None){ moveLeft = KeyCode.A; }
		if(moveRight == KeyCode.None){ moveRight = KeyCode.D; }
		if(moveBackward == KeyCode.None){ moveBackward = KeyCode.S; }
		if(replay == KeyCode.None){ replay = KeyCode.Escape; }
	}

}
