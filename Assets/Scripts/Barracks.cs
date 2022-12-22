using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    private Node soliderSpawnNode;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private List<ActionData> nextActions;
    [SerializeField] private LineRenderer lineRenderer;

    private Vector3[] lineRendStartPoints;
    private Vector3 spawnPointStartPosition;

    public override bool TryPlacement()
    {
        var finalResult = base.TryPlacement();

        if (finalResult)
        {
            lineRenderer.useWorldSpace = true;
            FindClosestEmptyNode();
            ChangeActions();
        }
        
        return finalResult;
    }

    public override bool TryChangeSpawnPoint()
    {
        base.TryChangeSpawnPoint();
        if (CustomGrid.Instance.NodeFromWorldPoint(spawnPoint.transform.position).walkable)
        {
            return true;
        }

        return false;
    }

    public override void ResetLineRendererToFirst()
    {
        lineRenderer.positionCount = lineRendStartPoints.Length;
        lineRenderer.SetPositions(lineRendStartPoints);
        spawnPoint.transform.position = spawnPointStartPosition;
    }

    public override void ChangeActions()
    {
        possibleActions = new List<ActionData>(nextActions);
        nextActions.Clear();
    }

    public override GameObject GetSpawnPointObj()
    {
        base.GetSpawnPointObj();
        SaveStartPositions();
        return spawnPoint;
    }

    public override void DoPathfinding(bool startFollow, Vector3 targetPos)
    {
        base.DoPathfinding(startFollow,targetPos);
        PathRequestManager.RequestPath(transform.position,targetPos, OnPathFound);
    }
    
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            
            Vector3[] newarr = new Vector3[newPath.Length + 1];
            for (int i = 1; i < newarr.Length; i++)
            {
                newarr[i] = newPath[i - 1];
            }

            newarr[0] = transform.position;
            lineRenderer.positionCount = newarr.Length;
            lineRenderer.SetPositions(newarr);
        }
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

    private void SaveStartPositions()
    {
        Array.Resize(ref lineRendStartPoints,lineRenderer.positionCount);
        lineRenderer.GetPositions(lineRendStartPoints);
        spawnPointStartPosition = spawnPoint.transform.position;
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
                        DoPathfinding(false,spawnPoint.transform.position);
                        return;
                    }
                }
            }

            nodesToTest = new List<Node>(nextNodes);
            nextNodes.Clear();
        }
    }
}
