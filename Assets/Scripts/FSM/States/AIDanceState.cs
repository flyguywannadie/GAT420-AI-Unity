using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState
{
	public AIDanceState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.animator.SetTrigger("gETdOWN"); ;
		Debug.Log("Dance enter");
	}

	public override void OnUpdate()
	{
		if (agent.enemyDistance < 2)
		{
			agent.stateMachine.SetState(nameof(AIFleeState));
		}

		Debug.Log("Dance update");
	}

	public override void OnExit()
	{
		Debug.Log("Dance exit");
	}
}
