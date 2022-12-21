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
    
        // Reference to name label.
        [SerializeField]
        private TextMeshProUGUI nameLabel;
    
        // Reference to image label.
        [SerializeField]
        private Image imageLabel;
        
        // Reference to the button.
        [SerializeField]
        private Button actionButton;

        public UnityEvent buttonClickedEvent;
        public ActionData currentReferenceData { get; private set; }
    
        public void SetupView(ActionData currentReferenceData)
        {
            this.currentReferenceData = currentReferenceData;
            nameLabel.text = currentReferenceData.objName;
            imageLabel.sprite = currentReferenceData.sprite;
        }

        public void SetActionType(ActionType actionType)
        {
            this.actionType = actionType;
        }
    
        public ActionType GetActionType()
        {
            return actionType;
        }

        public Button GetButton()
        {
            return actionButton;
        }
        
        /// <summary>
        /// Actions on click event handle.
        /// </summary>
        public void CustomButtonClickEvent()
        {
            buttonClickedEvent?.Invoke();
        }

        public virtual void StartAction()
        {
        
        }
    
        public virtual void DoAction()
        {
        
        }

        public virtual void CancelAction()
        {
        
        }
    }
}
