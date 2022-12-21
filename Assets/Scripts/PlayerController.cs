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
        selectedInteractable.ChangeSprite(Color.green);
        RootController.Instance.EngageController(RootController.ControllerTypeEnum.Information);
        RootController.Instance.SetupInfoPanel(selectedInteractable.getInteractableData);
        RootController.Instance.SetupActionsPanel(selectedInteractable.getActionsData);

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
        selectedInteractable = null;
    }

    IEnumerator WaitForUserInput()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RootController.Instance.TriggerCurrentAction();
            }

            yield return null;
        }
    }
}
