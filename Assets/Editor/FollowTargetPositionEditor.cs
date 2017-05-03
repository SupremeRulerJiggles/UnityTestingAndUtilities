using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowTargetPosition))]
public class FollowTargetPositionEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		FollowTargetPosition mainScript = (FollowTargetPosition)target;
		if(GUILayout.Button("Move To Target"))
		{
			mainScript.MoveToTargetPosition();
		}
	}
}
