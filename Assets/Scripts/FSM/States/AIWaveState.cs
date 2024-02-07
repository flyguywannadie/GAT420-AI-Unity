using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState
{
	float timer;

	public AIWaveState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		timer = Time.time + Random.Range(1, 2);
		Debug.Log("Wave enter");
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

		Debug.Log("Wave update");
	}

	public override void OnExit()
	{
		Debug.Log("Wave exit");
	}
}
