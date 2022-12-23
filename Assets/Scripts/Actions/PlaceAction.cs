using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class PlaceAction : BaseAction
    {
        private RaycastHit hit;
        private Coroutine _coroutine;
        
        public override void StartAction()
        {
            base.StartAction();
            
            if (_coroutine != null)
            {
                StopCoroutine(nameof(WaitForPlaceInput));
            }
            _coroutine = StartCoroutine(nameof(WaitForPlaceInput));
        }

        public override bool DoAction()
        {
            base.DoAction();
            var actionComplete = PlayerController.Instance.selectedInteractable.TryPlacement();

            if (actionComplete)
            {
                StopCoroutine(nameof(WaitForPlaceInput));
                PlayerController.Instance.selectedInteractable.UpdateGridPartColors(true);
            }
            return actionComplete;
        }

        public override void CancelAction()
        {
            base.CancelAction();
            StopCoroutine(nameof(WaitForPlaceInput));
            
            if (!PlayerController.Instance.selectedInteractable.CheckIfAlreadyPlaced())
            {
                PlayerController.Instance.selectedInteractable.gameObject.SetActive(false);
            }
        }
        
        IEnumerator WaitForPlaceInput()
        {
            Node lastGrid = null;
            
            while (true)
            {
                Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
                {
                    var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
                    
                    if (lastGrid != hoveringGrid)
                    {
                        PlayerController.Instance.selectedInteractable.transform.position = hoveringGrid.worldPosition;
                        PlayerController.Instance.selectedInteractable.UpdateGridPartColors(false);
                        lastGrid = hoveringGrid;
                    }
                }
                else
                {
                    PlayerController.Instance.selectedInteractable.transform.position = CameraMain.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f);
                }
             
                yield return null;
            }
        }
    }
}