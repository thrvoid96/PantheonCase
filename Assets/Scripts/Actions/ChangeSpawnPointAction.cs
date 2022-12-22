using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class ChangeSpawnPointAction : BaseAction
    {
        private RaycastHit hit;
        private Coroutine _coroutine;
        private Vector3 startPos;
        private GameObject spawnPointObj;
        public override void StartAction()
        {
            base.StartAction();
            
            if (_coroutine != null)
            {
                StopCoroutine(nameof(CheckForSpawnPoint));
            }
            _coroutine = StartCoroutine(nameof(CheckForSpawnPoint));
        }

        public override bool DoAction()
        {
            base.DoAction();
            StopCoroutine(nameof(CheckForSpawnPoint));
            PlayerController.Instance.selectedInteractable.SetupLineRenderer(spawnPointObj.transform.position);
            PlayerController.Instance.UnselectInteractable();
            return true;
        }

        public override void CancelAction()
        {
            base.CancelAction();
            StopCoroutine(nameof(CheckForSpawnPoint));
            spawnPointObj.transform.position = startPos;
        }
        
        IEnumerator CheckForSpawnPoint()
        {
            spawnPointObj = PlayerController.Instance.selectedInteractable.GetSpawnPointObj();
            startPos = spawnPointObj.transform.position;
            while (true)
            {
                Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
                {
                    var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
                    spawnPointObj.transform.position = hoveringGrid.worldPosition;
                    PlayerController.Instance.selectedInteractable.DoPathfinding(false,hit.point);
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
