using System;
using System.Collections;
using System.Collections.Generic;
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
        RootController.Instance.SetupInfoPanel(selectedInteractable.getInteractableData);
        RootController.Instance.SetupActionsPanel(selectedInteractable.getActionsData);
    }

    public void UnselectInteractable()
    {
        selectedInteractable = null;
    }

    private void Update()
    {
        if (selectedInteractable!=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
            
            }
        }
        
    }
}
