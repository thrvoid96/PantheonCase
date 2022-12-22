using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    private Node soliderSpawnNode;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<ActionData> nextActions;

    public override bool TryPlacement()
    {
        var finalResult = base.TryPlacement();

        if (finalResult)
        {
            FindClosestEmptyNode();
            ChangeActions();
        }
        
        return finalResult;
    }

    public override void ChangeSpawnPoint(Node nodeToTest)
    {
        base.ChangeSpawnPoint(nodeToTest);
        if (!nodeToTest.walkable)
        {
            FindClosestEmptyNode();
        }
    }

    public override void ChangeActions()
    {
        possibleActions = new List<ActionData>(nextActions);
        nextActions.Clear();
    }

    public override GameObject GetSpawnPointObj()
    {
        base.GetSpawnPointObj();
        return spawnPoint;
    }

    private void FindClosestEmptyNode()
    {
        if (soliderSpawnNode == null)
        {
            TestForNodes(spawnPoint.transform.position);
        }
        else
        {
            TestForNodes(soliderSpawnNode.worldPosition);
        }
    }

    private void TestForNodes(Vector3 positionToStartTest)
    {
        List<Node> nodesToTest = new List<Node>();
        List<Node> nextNodes = new List<Node>();
        nodesToTest.Add(CustomGrid.Instance.NodeFromWorldPoint(positionToStartTest));
        while (soliderSpawnNode == null)
        {
            foreach (Node node in nodesToTest)
            {
                nextNodes = CustomGrid.Instance.GetNeighbours(node);
                foreach (Node startNode in nextNodes)
                {
                    if (startNode.walkable)
                    {
                        soliderSpawnNode = startNode;
                        spawnPoint.transform.position = soliderSpawnNode.worldPosition;
                        return;
                    }
                }
            }

            nodesToTest = new List<Node>(nextNodes);
            nextNodes.Clear();
        }
    }
}
