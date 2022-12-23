using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class Unit : Interactable
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private LineRenderer lineRenderer;
        Vector3[] path;
        int targetIndex;
        private bool successfullPathFound;
        private bool destinationReached;

        public override bool TryPathfinding(bool startFollow,Vector3 targetPos)
        {
            base.TryPathfinding(startFollow,targetPos);
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

            return successfullPathFound;
        }

        public void FollowPath(Vector3[] newPath, bool pathSuccessful)
        {
            successfullPathFound = pathSuccessful;
            if (pathSuccessful) {
                path = newPath;
                targetIndex = 1;
                SetLineRendererPoints();
            
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
                targetIndex = 1;
                SetLineRendererPoints();
            
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else
            {
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
            }
        }

        IEnumerator StartFollow() {
            Vector3 currentWaypoint = path[1];
            while (true) {
                if (Vector3.Distance(currentWaypoint, transform.position) < 0.1f) {
                
                    targetIndex+=1;
                    ShiftLineRendererPoints();
                
                    if (targetIndex >= path.Length) {
                        destinationReached = true;
                        currentlyDoingAction = false;
                        CanBeInteracted(true);
                        yield break;
                    }
                
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
                lineRenderer.SetPosition(0,transform.position);
                yield return null;
            }
        }


        private void SetLineRendererPoints()
        {
            lineRenderer.positionCount = path.Length;
            lineRenderer.SetPositions(path);
        }

        private void ShiftLineRendererPoints()
        {
            int newPositionCount = lineRenderer.positionCount - 1;
            //Debug.LogError(newPositionCount);
            Vector3[] newPositions = new Vector3[newPositionCount];
 
            for (int i = 0; i < newPositionCount; i++){
                newPositions[i] = lineRenderer.GetPosition(i + 1);
            }

            lineRenderer.positionCount = newPositionCount;
            lineRenderer.SetPositions(newPositions);
        }
        
        public override void ClearPath()
        {
            path = null;
        }

    }
}