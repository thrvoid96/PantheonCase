using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class PlaceAction : BaseAction
    {
        public override void StartAction()
        {
            base.StartAction();
        }

        public override void DoAction()
        {
            base.DoAction();
        }

        public override void CancelAction()
        {
            base.CancelAction();
        }
        
        // IEnumerator WaitForPlaceInput()
        // {
        //     while (true)
        //     {
        //         Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
        //         RaycastHit hit;
        //         if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
        //         {
        //             var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
        //             selectedBuilding.transform.position = hoveringGrid.worldPosition;
        //             if (Input.GetMouseButtonDown(1))
        //             {
        //                 var placementComplete = selectedBuilding.TryPlaceBuilding();
        //                 if (placementComplete)
        //                 {
        //                     selectedBuilding = null;
        //                     yield break;
        //                 }
        //             }
        //         }
        //         else
        //         {
        //         
        //             selectedBuilding.transform.position = CameraMain.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f);
        //         }
        //      
        //         yield return null;
        //     }
        // }
    }
}