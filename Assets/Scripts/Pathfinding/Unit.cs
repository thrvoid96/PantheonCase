using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Interactable
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LineRenderer lineRenderer;
    Vector3[] path;
    int targetIndex;
    private bool successfullPathFound;
    private bool destinationReached;

    public override void DoPathfinding(bool startFollow,Vector3 targetPos)
    {
        base.DoPathfinding(startFollow,targetPos);
        currentlyDoingAction = true;
        if (startFollow)
        {
            destinationReached = false;
            PathRequestManager.RequestPath(transform.position,targetPos, FollowPath);
        }
        else
        {
            PathRequestManager.RequestPath(transform.position,targetPos, OnPathFound);
        }
    }

    public void FollowPath(Vector3[] newPath, bool pathSuccessful)
    {
        successfullPathFound = pathSuccessful;
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            
            Vector3[] newarr = new Vector3[newPath.Length + 1];
            for (int i = 1; i < newarr.Length; i++)
            {
                newarr[i] = newPath[i - 1];
            }
            
            newarr[0] = transform.position;
            lineRenderer.positionCount = newarr.Length;
            lineRenderer.SetPositions(newarr);
            
            //CustomGrid.Instance.NodeFromWorldPoint(transform.position).walkable = true;
            //CustomGrid.Instance.NodeFromWorldPoint(path[path.Length - 1]).walkable = false;
            StopCoroutine(nameof(StartFollow));
            StartCoroutine(nameof(StartFollow));
        }
        else
        {
            path = null;
        }
    }
    
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        successfullPathFound = pathSuccessful;
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            
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

    IEnumerator StartFollow() {
        Vector3 currentWaypoint = path[0];
        while (true) {
            if (transform.position == currentWaypoint) {
                targetIndex ++;
                
                if (targetIndex >= path.Length) {
                    destinationReached = true;
                    currentlyDoingAction = false;
                    CanBeInteracted(true);
                    yield break;
                }
                currentWaypoint = path[targetIndex];
                ShiftLineRendererPoints();
            }

            transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
            lineRenderer.SetPosition(0,transform.position);
            yield return null;
        }
    }

    private void ShiftLineRendererPoints()
    {
        int newPositionCount = lineRenderer.positionCount - 1;
        Vector3[] newPositions = new Vector3[newPositionCount];
 
        for (int i = 0; i < newPositionCount; i++){
            newPositions[i] = lineRenderer.GetPosition(i + 1);
        }
 
        lineRenderer.SetPositions(newPositions);
    }
    
    // public void OnDrawGizmos() {
    //     if (path != null) {
    //         if (successfullPathFound)
    //         {
    //             Gizmos.color = Color.black;
    //         }
    //         else
    //         {
    //             Gizmos.color = Color.red;
    //         }
    //         for (int i = targetIndex; i < path.Length; i ++) {
    //             Gizmos.DrawCube(path[i], Vector3.one);
    //
    //             if (i == targetIndex) {
    //                 Gizmos.DrawLine(transform.position, path[i]);
    //             }
    //             else {
    //                 Gizmos.DrawLine(path[i-1],path[i]);
    //             }
    //         }
    //     }
    // }

    public override void ClearPath()
    {
        path = null;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }
    
}