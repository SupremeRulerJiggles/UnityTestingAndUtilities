using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour 
{
	public float Speed = 1f;

	InputHandler inputHandler;

	void Start () 
	{
		try{ inputHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inputHandler; }	
		catch{ Debug.Log(MessageText.managerError + "Could not find the GM!", gameObject); }
	}

	public void Move(Vector3 dir)
	{
		try{ GetComponent<Rigidbody>().velocity = dir * Speed; }
		catch{ Debug.Log(MessageText.managerError + "Tried to move an object without a rigidBody", gameObject); }
	}
}
