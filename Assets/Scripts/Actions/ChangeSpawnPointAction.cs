using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class ChangeSpawnPointAction : BaseAction
    {
        private RaycastHit hit;
        private GameObject spawnPointObj;
        public override void StartAction()
        {
            base.StartAction();
            
            StopCoroutine(nameof(CheckForSpawnPoint));
            StartCoroutine(nameof(CheckForSpawnPoint));
        }

        public override bool DoAction()
        {
            base.DoAction();
            
            var actionComplete = PlayerController.Instance.selectedInteractable.TryChangeSpawnPoint();
            if (actionComplete)
            {
                StopCoroutine(nameof(CheckForSpawnPoint));
            }

            return actionComplete;
        }

        public override void CancelAction()
        {
            base.CancelAction();
            StopCoroutine(nameof(CheckForSpawnPoint));
            PlayerController.Instance.selectedInteractable.ResetLineRendererToFirst();
        }
        
        IEnumerator CheckForSpawnPoint()
        {
            Node lastGrid = null;
            spawnPointObj = PlayerController.Instance.selectedInteractable.GetSpawnPointObj();
            while (true)
            {
                Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
                {
                    var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
                    if (lastGrid != hoveringGrid)
                    {
                        spawnPointObj.transform.position = hoveringGrid.worldPosition;
                        PlayerController.Instance.selectedInteractable.TryPathfinding(false,hit.point);
                        lastGrid = hoveringGrid;
                    }
                   
                }
                else
                {
                    spawnPointObj.transform.position = CameraMain.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f);
                }
                
                yield return null;
            }
        }

    }
}
