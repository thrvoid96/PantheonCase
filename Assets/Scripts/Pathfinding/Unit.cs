using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : Interactable
{
    [SerializeField] private float speed = 5f;
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
        }
        else
        {
            path = new[]
            {
                transform.position
            };
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
            }

            transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
            yield return null;
        }

        
    }
    
    public void OnDrawGizmos() {
        if (path != null) {
            if (successfullPathFound)
            {
                Gizmos.color = Color.black;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            for (int i = targetIndex; i < path.Length; i ++) {
                Gizmos.DrawCube(path[i], Vector3.one);
    
                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i-1],path[i]);
                }
            }
        }
    }

    public override void ClearPath()
    {
        path = null;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }
    
}