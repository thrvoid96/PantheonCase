using System;
using System.Collections.Generic;
using Actions;
using MVC.Views;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controller responsible for game phase.
/// </summary>
public class ActionsController : SubController<ActionsView>
{
    
    private void Awake()
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
    public void SetupActionsView(List<BaseAction> actions)
    {
        view.ShowActions(actions);
    }
    

}