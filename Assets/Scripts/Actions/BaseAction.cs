using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        // Reference to action type.
        [SerializeField]
        private ActionType actionType = ActionType.Placeholder;
        
        [SerializeField] private ActionData actionData;
        public ActionData getActionData => actionData;

        private ActionView actionView;
        public ActionView getActionView => actionView;
    
        public void AttachToView(ActionView actionView)
        {
            this.actionView = actionView;
            actionView.ConnectToView(actionData);
        }
        
        public ActionType GetActionType()
        {
            return actionType;
        }

        public void RegisterToEvents(UnityAction callback)
        {
            actionView.AddListenerToEvent(callback);
        }
        
        public void RemoveFromEvents(UnityAction callback)
        {
            actionView.RemoveListenerFromEvent(callback);
        }

        public void ClearAllEvents()
        {
            actionView.RemoveAllListeners();
        }
        
        public virtual void StartAction()
        {
        
        }
    
        public virtual bool DoAction()
        {
            return false;
        }

        public virtual void CancelAction()
        {
        
        }
    }
}
