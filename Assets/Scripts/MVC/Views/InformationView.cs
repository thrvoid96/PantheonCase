using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    /// <summary>
    /// Game over view with events for buttons and showing data.
    /// </summary>
    public class InformationView : BaseUIView
    {
        // Reference to name label.
        [SerializeField]
        private TextMeshProUGUI nameLabel;

        // Reference to description label.
        [SerializeField]
        private TextMeshProUGUI descriptionLabel;
    
        // Reference to description label.
        [SerializeField]
        private Image imageLabel;
    
        // Reference to description label.
        [SerializeField]
        private ScrollRect scrollRect;

        /// <summary>
        /// Method used to show information data in UI.
        /// </summary>
        /// <param name="interactableData">interactable data.</param>
        /// /// <param name="actionData">action data.</param>
        public void ShowInformation(InteractableData interactableData)
        {
            nameLabel.text = interactableData.name;
            descriptionLabel.text = interactableData.description;
            imageLabel.sprite = interactableData.sprite;
        }

        private void ResetAllActions()
        {
        
        }
    }
}