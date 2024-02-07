using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
	public AIAttackState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.velocity = Vector3.zero;
		//agent.movement.destination = agent.transform.position;
		agent.animator?.SetTrigger("Attack");
		agent.timer.value = 2;
		Debug.Log("attack enter");
	}

	public override void OnUpdate()
	{
	}

	public override void OnExit()
	{
		Debug.Log("attack exit");
	}
}
