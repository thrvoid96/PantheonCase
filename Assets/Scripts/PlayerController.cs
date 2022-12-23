using System.Collections;
using Interactables;
using MVC.Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    public Interactable selectedInteractable { get; private set; }

    [HideInInspector]public UnityEvent playerInputGiven;
    
    public void SelectNewInteractable(Interactable interactable)
    {
        if (selectedInteractable == null)
        {
            selectedInteractable = interactable;
            selectedInteractable.CanBeInteracted(false);

            RootController.Instance.EngageController(RootController.ControllerTypeEnum.Information);
            RootController.Instance.SetupInfoPanel(selectedInteractable.getInteractableData);
            RootController.Instance.SetupActionsPanel(selectedInteractable.getActions);
        }
    }

    public void UnselectInteractable()
    {
        RootController.Instance.DisengageController(RootController.ControllerTypeEnum.Information);
        
        if (!selectedInteractable.currentlyDoingAction)
        {
            selectedInteractable.CanBeInteracted(true);
        }
        
        selectedInteractable.ClearPath();
        selectedInteractable = null;
    }

    public void FireInputEvent(InputAction.CallbackContext context)
    {
        if (selectedInteractable != null && context.performed)
        {
            playerInputGiven?.Invoke();
        }
    }
    
}
