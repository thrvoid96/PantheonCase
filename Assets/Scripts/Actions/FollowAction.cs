using System.Collections;
using UnityEngine;

namespace Actions
{
    public class FollowAction : BaseAction
    {
        private RaycastHit hit;
        private Coroutine _coroutine;
        public override void StartAction()
        {
            base.StartAction();
            
            if (_coroutine != null)
            {
                StopCoroutine(nameof(CheckForAvailablePath));
            }
            _coroutine = StartCoroutine(nameof(CheckForAvailablePath));
        }

        public override bool DoAction()
        {
            base.DoAction();
            StopCoroutine(nameof(CheckForAvailablePath));
            PlayerController.Instance.selectedInteractable.DoPathfinding(true,hit.point);
            return true;
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
                    PlayerController.Instance.selectedInteractable.DoPathfinding(false,hit.point); 
                }   
             
                yield return null;
            }
        }
        

    }
}
