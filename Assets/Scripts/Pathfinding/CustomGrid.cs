using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

public class CustomGrid : Singleton<CustomGrid>
{
	public bool displayGridGizmos;

	[VectorRange(1f, 100f, 1f, 100f)] public Vector2 gridSize = Vector2.one;
	
	[Range(1f,100f)]
	public float nodeDiameter = 1f;

	Node[,] grid;

	float nodeRadius;
	int gridSizeX, gridSizeY;
	
#if UNITY_EDITOR
	private void OnValidate()
	{
		transform.localScale = new Vector3(gridSize.x, 0, gridSize.y) * 0.1f * nodeDiameter;
		GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_GridSizeX",1f / gridSize.x);
		GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_GridSizeY",1f / gridSize.y);
	}
#endif

	void Awake()
	{
		nodeRadius = nodeDiameter * 0.5f;
		gridSizeX = Mathf.RoundToInt(gridSize.x);
		gridSizeY = Mathf.RoundToInt(gridSize.y);
		CreateGrid();
	}

	public int MaxSize
	{
		get { return gridSizeX * gridSizeY; }
	}

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft =
			transform.position - (Vector3.right * gridSizeX * nodeRadius) - (Vector3.up * gridSizeY * nodeRadius) ;
		
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = (worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
				                      Vector3.up * (y * nodeDiameter + nodeRadius));
				grid[x, y] = new Node(true, worldPoint, x, y);
			}
		}
	}

	public void SetWalkable(Node node, bool value)
	{
		node.walkable = value;
	}

	public Node[,] GetAllNodes()
	{
		return grid;
	}
	
	public Node GetNode(int gridX, int gridY)
	{
		return grid[gridX,gridY];
	}

	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbours.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbours;
	}


	public Node NodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridSize.x / 2) / gridSize.x;
		float percentY = (worldPosition.y + gridSize.y / 2) / gridSize.y;
		
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x * transform.localScale.x, gridSize.y * transform.localScale.y, 0.5f));
		if (grid != null && displayGridGizmos)
		{
			foreach (Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
			}
		}
	}
}