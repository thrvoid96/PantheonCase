using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionView : MonoBehaviour
{
    // Reference to name label.
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    
    // Reference to image label.
    [SerializeField]
    private Image imageLabel;
        
    // Reference to the button.
    [SerializeField]
    private Button actionButton;
    
     [HideInInspector] public UnityEvent buttonClickedEvent;
    
    public void ConnectToView(ActionData referenceData)
    {
        nameLabel.text = referenceData.objName;
        imageLabel.sprite = referenceData.sprite;
    }
    
    public void AddListenerToEvent(UnityAction method)
    {
        buttonClickedEvent.AddListener(method);
    }
    
    public void RemoveListenerFromEvent(UnityAction method)
    {
        buttonClickedEvent.RemoveListener(method);
    }

    public void RemoveAllListeners()
    {
        buttonClickedEvent.RemoveAllListeners();
    }

    public void TriggerEvent()
    {
        buttonClickedEvent?.Invoke();
    }

    public void HighlightButton()
    {
        var actionButtonColors = actionButton.colors;
        actionButtonColors.normalColor = Color.yellow;
        actionButton.colors = actionButtonColors;
    }
    
    public void ClearButtonHighlight()
    {
        var actionButtonColors = actionButton.colors;
        actionButtonColors.normalColor = Color.white;
        actionButton.colors = actionButtonColors;
    }
}
