using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseAction : MonoBehaviour
{
    // Reference to action type.
    [SerializeField]
    private ActionType actionType;
    
    // Reference to name label.
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    
    // Reference to image label.
    [SerializeField]
    private Image imageLabel;

    public ActionData currentReferenceData { get; private set; }
    
    public void SetupView(ActionData currentReferenceData)
    {
        this.currentReferenceData = currentReferenceData;
        nameLabel.text = currentReferenceData.name;
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
    
    public virtual void DoAction()
    {
        
    }
}
