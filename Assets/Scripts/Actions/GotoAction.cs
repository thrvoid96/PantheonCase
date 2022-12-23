using System.Collections;
using UnityEngine;

namespace Actions
{
    public class GotoAction : BaseAction
    {
        private RaycastHit hit;
        public override void StartAction()
        {
            base.StartAction();
            
            StopCoroutine(nameof(CheckForAvailablePath));
            StartCoroutine(nameof(CheckForAvailablePath));
        }

        public override bool DoAction()
        {
            base.DoAction();
            var pathViable = PlayerController.Instance.selectedInteractable.TryPathfinding(true,hit.point);
            if (pathViable)
            {
                StopCoroutine(nameof(CheckForAvailablePath));
                PlayerController.Instance.UnselectInteractable();
            }
            return pathViable;
        }

        public override void CancelAction()
        {
            base.CancelAction();
            StopCoroutine(nameof(CheckForAvailablePath));
        }
        
        IEnumerator CheckForAvailablePath()
        {
            while (true)
            {
                Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid"))) 
                {
                    PlayerController.Instance.selectedInteractable.TryPathfinding(false,hit.point); 
                }   
             
                yield return null;
            }
        }
        

    }
}
