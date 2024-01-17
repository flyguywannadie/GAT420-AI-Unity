using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AIRaycastPerception : AIPerception
{
	[SerializeField, Range(2,50)] private int numRaycast = 2;

	public override GameObject[] GetGameObjects()
	{
		List<GameObject> result = new List<GameObject>();

		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, MaxAngle);
		foreach (Vector3 direction in directions)
		{
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			if (Physics.Raycast(ray, out RaycastHit hit, Distance, LayerMask))
			{
				Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
				if (hit.collider.gameObject == gameObject) continue;

				foreach (string tagname in TagName)
				{
					if (tagname == "" || hit.collider.CompareTag(tagname))
					{
						result.Add(hit.collider.gameObject);
					}
				}
			}
			else
			{
				Debug.DrawRay(ray.origin, ray.direction * Distance, Color.blue);
			}
		}

		return result.Distinct().ToList().ToArray();
	}

	public bool GetOpenDirection(ref Vector3 openDirection)
	{
		Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, MaxAngle);
		foreach (var direction in directions)
		{
			// cast ray from transform position towards direction (use game object orientation)
			Ray ray = new Ray(transform.position, transform.rotation * direction);
			// if there is NO raycast hit then that is an open direction
			if (!Physics.Raycast(ray, out RaycastHit raycastHit, Distance, LayerMask))
			{
				Debug.DrawRay(ray.origin, ray.direction * Distance, Color.green);
				// set open direction
				openDirection = ray.direction;
				return true;
			}
		}

		// no open direction
		return false;
	}

	public bool CheckDirection(Vector3 direction)
	{
		// create ray in direction (use game object orientation)
		Ray ray = new Ray(transform.position, transform.rotation * direction);
		// check ray cast
		return Physics.Raycast(ray, Distance, LayerMask);

	}
}
