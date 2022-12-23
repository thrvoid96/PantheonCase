using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class Barracks : Building
{
    private Node soliderSpawnNode;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private LineRenderer lineRenderer;

    private Vector3[] lineRendStartPoints, path;
    private Vector3 spawnPointStartPosition;
    private bool successfullPathFound;

    public override bool TryPlacement()
    {
        var finalResult = base.TryPlacement();

        if (finalResult)
        {
            lineRenderer.useWorldSpace = true;
            FindClosestEmptyNode();
        }
        
        return finalResult;
    }

    public override bool TryChangeSpawnPoint()
    {
        base.TryChangeSpawnPoint();
        if (CustomGrid.Instance.NodeFromWorldPoint(spawnPoint.transform.position).walkable)
        {
            lineRendStartPoints = path;
            spawnPointStartPosition = spawnPoint.transform.position;
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
    
    public override GameObject GetSpawnPointObj()
    {
        base.GetSpawnPointObj();
        SaveStartPositions();
        return spawnPoint;
    }

    public override bool TryPathfinding(bool startFollow, Vector3 targetPos)
    {
        base.TryPathfinding(startFollow,targetPos);
        PathRequestManager.RequestPath(transform.position,targetPos, OnPathFound);
        return successfullPathFound;
    }
    
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        successfullPathFound = pathSuccessful;
        if (pathSuccessful)
        {
            path = newPath;
            lineRenderer.positionCount = newPath.Length;
            lineRenderer.SetPositions(newPath);
            lineRenderer.startColor = Color.yellow;
            lineRenderer.endColor = Color.yellow;
        }
        else
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
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
                        TryPathfinding(false,spawnPoint.transform.position);
                        return;
                    }
                }
            }

            nodesToTest = new List<Node>(nextNodes);
            nextNodes.Clear();
        }
    }

}
