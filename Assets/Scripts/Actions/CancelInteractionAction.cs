using UnityEngine;

namespace Actions
{
    public class CancelInteractionAction : BaseAction
    {
        public override void StartAction()
        {
            base.StartAction();
            PlayerController.Instance.UnselectInteractable();
        }

        public override bool DoAction()
        {
            base.DoAction();
            return true;
        }

        public override void CancelAction()
        {
            base.CancelAction();
        }
    }
}
