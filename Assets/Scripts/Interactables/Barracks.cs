using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
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
                lineRenderer.positionCount = 0;
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
            List<Node> testedNodes = new List<Node>();
            nodesToTest.Add(CustomGrid.Instance.NodeFromWorldPoint(positionToStartTest));
            while (soliderSpawnNode == null)
            {
                foreach (Node node in nodesToTest)
                {
                    if(testedNodes.Contains(node))
                    {
                        continue;
                    }
                
                    if (node.walkable)
                    {
                        SetupForNode(node);
                        return;
                    }

                    nextNodes = CustomGrid.Instance.GetNeighbours(node);

                    if (!testedNodes.Contains(node))
                    {
                        testedNodes.Add(node);
                    }
                }

                nodesToTest = new List<Node>(nextNodes);
                nextNodes.Clear();
            }
        }

        private void SetupForNode(Node node)
        {
            soliderSpawnNode = node;
            spawnPoint.transform.position = soliderSpawnNode.worldPosition;
            TryPathfinding(false,spawnPoint.transform.position);
        }
    }
}
