using UnityEngine;

namespace Actions
{
    public class CancelInteractionAction : BaseAction
    {
        public override void StartAction()
        {
            base.StartAction();
            PlayerController.Instance.selectedInteractable.ChangeSprite(Color.magenta);
            PlayerController.Instance.UnselectInteractable();
        }

        public override void DoAction()
        {
            base.DoAction();
        }

        public override void CancelAction()
        {
            base.CancelAction();
        }
    }
}
