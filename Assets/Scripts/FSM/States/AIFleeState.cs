using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFleeState : AIState
{
	float timer;

	public AIFleeState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		timer = Time.time + Random.Range(1, 2);
		Debug.Log("Flee enter");
	}

	public override void OnUpdate()
	{
		if (Time.time >= timer)
		{
			agent.stateMachine.SetState(nameof(AIPatrolState));
		}

		var enemies = agent.enemyPerception.GetGameObjects();

		if (enemies.Length > 0)
		{
			agent.stateMachine.SetState(nameof(AIChaseState));
		}

		Debug.Log("Flee update");
	}

	public override void OnExit()
	{
		Debug.Log("Flee exit");
	}
}
