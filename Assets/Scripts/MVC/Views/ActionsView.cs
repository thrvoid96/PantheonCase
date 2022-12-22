using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MVC.Views
{
    /// <summary>
    /// Game view with events for buttons and showing data.
    /// </summary>
    public class ActionsView : BaseUIView
    {
        [SerializeField] private ScrollRect scrollRect;
        
        // Reference to description label.
        [SerializeField]
        private TextMeshProUGUI descriptionLabel;
        
        [SerializeField] private ActionView cancelActionView;
        [SerializeField] private BaseAction cancelAction;
        private List<ActionView> availableActions = new List<ActionView>();

        private BaseAction selectedAction;

        public void ShowActions(List<BaseAction> actions)
        {
            ChangeAllActionsVisibility(false);
            MakeActionsVisible(actions);
        }
        
        public void InitialSetupForAvailableActions()
        {
            for (int i = 2; i < Enum.GetNames(typeof(ActionType)).Length; i++)
            {
                var spawnedAction = ObjectPool.Instance.SpawnFromPool("ActionView", Vector3.zero, Quaternion.identity, scrollRect.content)
                    .GetComponent<ActionView>();
                
                //Nedense spawn ederken scaleleri bozuluyor
                spawnedAction.transform.localScale = Vector3.one;
                availableActions.Add(spawnedAction);
            }
            
            availableActions.Add(cancelActionView);
            cancelAction.AttachToView(cancelActionView);
            ChangeAllActionsVisibility(false);
            PlayerController.Instance.playerInputGiven.AddListener(TriggerCurrentAction);
        }
        
        
        private void ChangeAllActionsVisibility(bool value)
        {
            for (int i = 0; i < availableActions.Count; i++)
            {
                availableActions[i].gameObject.SetActive(value);
            }
        }
        
        private void MakeActionsVisible(List<BaseAction> actions)
        {
            for (int i = 0; i < availableActions.Count - 1; i++)
            {
                availableActions[i].RemoveAllListeners();
            }
            
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].AttachToView(availableActions[i]);
                actions[i].RegisterToEvents(delegate { ChangeSelectedAction(actions[i]); });
                    
                availableActions[i].transform.SetSiblingIndex(i);
                availableActions[i].gameObject.SetActive(true);
            }
            
            cancelActionView.transform.SetSiblingIndex(actions.Count);
            cancelAction.gameObject.SetActive(true);

            ChangeSelectedAction(actions[0]);
            
            //LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
        }

        private void ChangeSelectedAction(BaseAction action)
        {
            if (selectedAction != null)
            {
                selectedAction.CancelAction();
            }
            
            selectedAction = action;
            selectedAction.StartAction();
            
            if (selectedAction.GetActionType() != ActionType.Cancel)
            {
                selectedAction.getActionView.GetButton().Select();
            }
            
            descriptionLabel.text = selectedAction.getActionData.description;
        }
        
        public void TriggerCurrentAction()
        {
            selectedAction.DoAction();
        }
    }
}