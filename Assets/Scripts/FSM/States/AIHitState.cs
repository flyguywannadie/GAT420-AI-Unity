using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIState
{
	float initialSpeed;
	public AIHitState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 1));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;
		agent.animator?.SetTrigger("Hit");
		agent.timer.value = 2;
		Debug.Log("Chase enter");
	}

	public override void OnUpdate()
	{

	}

	public override void OnExit()
	{
		Debug.Log("Chase exit");
	}
}
