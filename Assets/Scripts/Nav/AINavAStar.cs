using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class AINavAStar
{
	public static bool Generate(AINavNode startnode, AINavNode endnode, ref List<AINavNode> path)
	{
		var nodes = new SimplePriorityQueue<AINavNode>();

		startnode.Cost = 0;
		float heuristic = Vector3.Distance(startnode.transform.position, endnode.transform.position);
		nodes.EnqueueWithoutDuplicates(startnode, startnode.Cost);

		bool found = false;
		while (!found && nodes.Count > 0)
		{
			var node = nodes.Dequeue();

			if (node == endnode)
			{
				found = true;
				break;
			}

            foreach ( var neighbor in node.neighbors)
			{
				float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
				if (cost < neighbor.Cost)
				{
					neighbor.Cost = cost;
					neighbor.Parent = node;
					heuristic = Vector3.Distance(neighbor.transform.position, endnode.transform.position);
					nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost + heuristic);
				}
			}
		}

		path.Clear();
		if (found)
		{
			CreatePathFromParents2(endnode, ref path);
		}

		return found;
	}

	public static void CreatePathFromParents2(AINavNode node, ref List<AINavNode> path)
	{
		// while node not null
		while (node != null)
		{
			// add node to list path
			path.Add(node);
			// set node to node parent
			node = node.Parent;
		}

		// reverse path
		path.Reverse();
	}
}
