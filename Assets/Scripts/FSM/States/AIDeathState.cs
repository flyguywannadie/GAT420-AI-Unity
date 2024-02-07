using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIDeathState : AIState
{
	float timer = 0;

	public AIDeathState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.animator?.SetTrigger("Death");
		timer = Time.time + 2;
		Debug.Log("death enter");
	}

	public override void OnUpdate()
	{
		if (Time.time >= timer)
		{
			GameObject.Destroy(agent.gameObject);
		}
		Debug.Log("death update");
	}

	public override void OnExit()
	{
		Debug.Log("death exit");
	}
}
