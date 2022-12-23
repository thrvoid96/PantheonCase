using System.Collections;
using MVC.Controllers;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Singleton<PlayerController>
{
    private Coroutine _coroutine;
    public Interactable selectedInteractable { get; private set; }

    public UnityEvent playerInputGiven;
    
    public void SelectNewInteractable(Interactable interactable)
    {
        selectedInteractable = interactable;
        selectedInteractable.CanBeInteracted(false);

        RootController.Instance.EngageController(RootController.ControllerTypeEnum.Information);
        RootController.Instance.SetupInfoPanel(selectedInteractable.getInteractableData);
        RootController.Instance.SetupActionsPanel(selectedInteractable.getActions);


        if (_coroutine != null)
        {
            StopCoroutine(nameof(WaitForUserInput));
        }
       
        _coroutine = StartCoroutine(nameof(WaitForUserInput));
    }

    public void UnselectInteractable()
    {
        StopCoroutine(nameof(WaitForUserInput));
        RootController.Instance.DisengageController(RootController.ControllerTypeEnum.Information);
        
        if (!selectedInteractable.currentlyDoingAction)
        {
            selectedInteractable.CanBeInteracted(true);
        }
        
        selectedInteractable.ClearPath();
        selectedInteractable = null;
    }

    IEnumerator WaitForUserInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                playerInputGiven?.Invoke();
            }

            yield return null;
        }
    }
    
}
