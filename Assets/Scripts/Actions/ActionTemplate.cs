using UnityEngine;

namespace Actions
{
    public class ActionTemplate : BaseAction
    {
        public override void StartAction()
        {
            base.StartAction();
        }

        public override bool DoAction()
        {
            base.DoAction();
            return false;
        }

        public override void CancelAction()
        {
            base.CancelAction();
        }
    }
}
