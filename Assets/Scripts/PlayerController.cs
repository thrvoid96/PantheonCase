using System.Collections;
using MVC.Controllers;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Coroutine _coroutine;
    public Interactable selectedInteractable { get; private set; }
    
    public void SelectNewInteractable(Interactable interactable)
    {
        selectedInteractable = interactable;
        selectedInteractable.CanBeInteracted(false);
        //selectedInteractable.ChangeSprite(Color.green);
        RootController.Instance.EngageController(RootController.ControllerTypeEnum.Information);
        //Debug.LogError(selectedInteractable);
        RootController.Instance.SetupInfoPanel(selectedInteractable.getInteractableData);
        //Debug.LogError(selectedInteractable);
        RootController.Instance.SetupActionsPanel(selectedInteractable.getActionsData);
        //Debug.LogError(selectedInteractable);
        

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
        selectedInteractable.CanBeInteracted(true);
        selectedInteractable.ClearPath();
        selectedInteractable = null;
    }

    IEnumerator WaitForUserInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                bool actionCompleted = RootController.Instance.TriggerCurrentAction();
            }

            yield return null;
        }
    }
}
