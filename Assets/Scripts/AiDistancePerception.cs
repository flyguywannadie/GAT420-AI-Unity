using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiDistancePerception : AIPerception
{
    public override GameObject[] GetGameObjects()
    {
		List<GameObject> result = new List<GameObject>();

		Collider[] colliders = Physics.OverlapSphere(transform.position, Distance, LayerMask);
		foreach (Collider collider in colliders)
		{
			if (collider.gameObject == gameObject) continue;
			foreach (string tagname in TagName)
			{
				if (tagname == "" || collider.CompareTag(tagname))
				{
					// calculate angle from transform forward vector to direction of game object
					Vector3 direction = (collider.transform.position - transform.position).normalized;
					float angle = Vector3.Angle(transform.forward, direction);
					// if angle is less than max angle, add game object
					if (angle <= MaxAngle)
					{
						result.Add(collider.gameObject);
					}
				}
			}
		}

		return result.ToArray();
	}
}
