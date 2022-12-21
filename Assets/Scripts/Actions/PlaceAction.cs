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
                PlayerController.Instance.UnselectInteractable();
            }
            return actionComplete;
        }

        public override void CancelAction()
        {
            base.CancelAction();
            Debug.LogError("stopCanceling");
            StopCoroutine(nameof(WaitForPlaceInput));
            
            if (!PlayerController.Instance.selectedInteractable.CheckIfAlreadyPlaced())
            {
                PlayerController.Instance.selectedInteractable.gameObject.SetActive(false);
            }
        }
        
        IEnumerator WaitForPlaceInput()
        {
            while (true)
            {
                Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
                {
                    var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
                    PlayerController.Instance.selectedInteractable.transform.position = hoveringGrid.worldPosition;
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