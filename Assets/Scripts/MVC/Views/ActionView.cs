using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Game view with events for buttons and showing data.
/// </summary>
public class ActionView : UIView
{
    // Reference to time label.
    [SerializeField]
    private TextMeshProUGUI timeLabel;
    
    [SerializeField]
    private ActionType actionType;

    // Event called when Finish Button is clicked.
    public UnityAction OnActionClicked;

    /// <summary>
    /// Method called by Finish Button.
    /// </summary>
    public void ActionClick()
    {
        OnActionClicked?.Invoke();
    }
    

    // /// <summary>
    // /// Method used to update action timers.
    // /// </summary>
    // /// <param name="cooldown">Action cooldown time.</param>
    // public void UpdateCooldown(float cooldown)
    // {
    //     timeLabel.text = string.Format("{0:#00}:{1:00.000}", (int)(cooldown / 60), (cooldown % 60));
    // }
}