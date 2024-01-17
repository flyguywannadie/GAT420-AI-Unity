using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAutonomousAgent : AIAgent
{
    public AIPerception seekPerception = null;
    public AIPerception fleePerception = null;
    public AIPerception flockPerception = null;
    public AIPerception obsticalPerception = null;

	private void Start()
	{
		transform.localScale = Vector3.one * Random.Range(0.5f,2.0f);
	}

	private void Update()
	{
		if (seekPerception != null)
		{
			var gameObjects = seekPerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Seek(gameObjects[0]));
			}
		}
		if (fleePerception != null)
		{
			var gameObjects = fleePerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Flee(gameObjects[0]) * 2);
			}
		}
		if (flockPerception != null)
		{
			var gameObjects = flockPerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				movement.ApplyForce(Cohesion(gameObjects));
				movement.ApplyForce(Seperation(gameObjects, 5));
				movement.ApplyForce(Alignment(gameObjects));
			}
		}

		if (obsticalPerception != null)
		{
			if (((AISphereCastPerception)obsticalPerception).CheckDirection(Vector3.forward))
			{
				Vector3 open = Vector3.zero;
				if (((AISphereCastPerception)obsticalPerception).GetOpenDirection(ref open))
				{
					movement.ApplyForce(GetSteeringForce(open) * 5);
				}
			}
		}

		Vector3 acceleration = movement.acceleration;
		acceleration.y = 0;
		movement.acceleration = acceleration;

		transform.position = Utilities.Wrap(transform.position, new Vector3(-20, -0.1f, -20), new Vector3(20, 0.1f, 20));
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        return GetSteeringForce(direction);
    }

	private Vector3 Flee(GameObject target)
	{
		Vector3 direction = transform.position - target.transform.position;
		return GetSteeringForce(direction);
	}

	private Vector3 Cohesion(GameObject[] neighbors)
	{
		Vector3 positions = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			positions += neighbor.transform.position;
		}
		Vector3 center = positions / neighbors.Length;
		Vector3 direction = center - transform.position;
		return GetSteeringForce(direction);
	}

	private Vector3 Seperation(GameObject[] neighbors, float radius)
	{
		Vector3 seperation = Vector3.zero;
		foreach (var neighbor in neighbors)
		{
			Vector3 direction = (transform.position - neighbor.transform.position);
			if (direction.magnitude < radius)
			{
				seperation += direction / direction.sqrMagnitude;
			}
		}
		return GetSteeringForce(seperation);
	}

	private Vector3 Alignment(GameObject[] neighbors)
	{
		Vector3 velocities = Vector3.zero;
		foreach(var neighbor in neighbors)
		{
			velocities += neighbor.GetComponent<AIAgent>().movement.velocity;
		}
		Vector3 averageVelocity = velocities / neighbors.Length;
		return GetSteeringForce(averageVelocity);
	}

	private Vector3 GetSteeringForce(Vector3 direction)
	{
		Vector3 desired = direction.normalized * movement.maxSpeed;
		Vector3 steer = desired - movement.velocity;
		return Vector3.ClampMagnitude(steer, movement.maxForce);
	}
}
