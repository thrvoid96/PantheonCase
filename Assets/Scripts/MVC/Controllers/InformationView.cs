using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Game over view with events for buttons and showing data.
/// </summary>
public class InformationView : UIView
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
    public void ShowInformation(InteractableData interactableData, List<ActionData> actionDatas)
    { 
        //scoreLabel.text = gameData.gameScore.ToString("N0");
        //timeLabel.text = string.Format("{0:###0}:{1:00.000}", (int)(gameData.gameTime / 60), (gameData.gameTime % 60));
    }

    private void ResetAllActions()
    {
        
    }
}