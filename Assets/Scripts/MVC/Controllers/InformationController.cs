using MVC.Views;
using UnityEngine;

/// <summary>
/// Controller responsible for game phase.
/// </summary>
public class InformationController : SubController<InformationView>
{
    // Reference to current interactable data.
    private InteractableData interactableData;
    
    public override void EngageController()
    {
        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();
    }

    /// <summary>
    /// Handling info panel view.
    /// </summary>
    public void SetupInfoView(InteractableData interactableData)
    {
        this.interactableData = interactableData;
        view.ShowInformation(interactableData);
    }
}