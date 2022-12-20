using System;
using System.Collections.Generic;
using MVC.Views;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controller responsible for game phase.
/// </summary>
public class ActionsController : SubController<ActionsView>
{
    // Reference to current actions data.
    private List<ActionData> actionDatas;
    
    private void Start()
    {
        InitialSetup();
    }

    public override void EngageController()
    {
        base.EngageController();
        
        // Attaching UI events.
        //view.OnActionClicked += ChangeSelectedAction;
    }

    public override void DisengageController()
    {
        base.DisengageController();
        //view.OnActionClicked -= ChangeSelectedAction;
    }

    private void InitialSetup()
    {
        view.InitialSetupForAvailableActions();
    }

    /// <summary>
    /// Handling info panel view.
    /// </summary>
    public void SetupActionsView(List<ActionData> actionDatas)
    {
        this.actionDatas = new List<ActionData>(actionDatas);
        view.ShowActions(actionDatas);
        ChangeSelectedAction(actionDatas[0]);
    }
    
    public void ChangeSelectedAction(ActionData actionData)
    {
        
    }

}