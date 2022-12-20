using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
        
        [SerializeField] private ActionData cancelActionData;

        [Header("These prefabs must be in the same order as ActionType")]
        [SerializeField] private List<GameObject> actionPrefabs;
        
        private BaseAction cancelAction;

        private List<BaseAction> availableActions = new List<BaseAction>();
        private List<BaseAction> currentActions = new List<BaseAction>();

        // // Event called when Finish Button is clicked.
        // public UnityAction OnActionClicked;
        //
        // /// <summary>
        // /// Method called by Finish Button.
        // /// </summary>
        // public void ActionClick()
        // {
        //     OnActionClicked?.Invoke();
        // }

        public void ShowActions(List<ActionData> actionDatas)
        {
            ChangeAllActionsVisibility(false);
            
            MakeActionsVisible(actionDatas);
        }
        
        public void InitialSetupForAvailableActions()
        {
            for (int i = 0; i < Enum.GetNames(typeof(ActionType)).Length - 2; i++)
            {
                var spawnedAction = ObjectPool.Instance.SpawnFromPool("Action",Vector3.zero, Quaternion.identity, scrollRect.content).GetComponent<BaseAction>();
                //Nedense spawn ederken scaleleri bozuluyor
                spawnedAction.transform.localScale = Vector3.one;
                spawnedAction.SetActionType((ActionType)i);
                availableActions.Add(spawnedAction);
            }
            
            AddCancelAction();
            //LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
            ChangeAllActionsVisibility(false);
        }
        
        private void AddCancelAction()
        {
            cancelAction = ObjectPool.Instance.SpawnFromPool("Action", Vector3.zero, Quaternion.identity, scrollRect.content).GetComponent<BaseAction>();
            cancelAction.SetupView(cancelActionData);
            //Nedense spawn ederken scaleleri bozuluyor
            cancelAction.transform.localScale = Vector3.one;
            availableActions.Add(cancelAction);
        }
        
        private void ChangeAllActionsVisibility(bool value)
        {
            for (int i = 0; i < availableActions.Count; i++)
            {
                availableActions[i].gameObject.SetActive(value);
            }
        }
        
        private void MakeActionsVisible(List<ActionData> actionDatas)
        {
            currentActions.Clear();

            for (int i = 0; i < actionDatas.Count; i++)
            {
                var actionToUse = availableActions.Find(m => m.GetActionType() == actionDatas[i].actionType);
                actionToUse.SetupView(actionDatas[i]);
                actionToUse.gameObject.SetActive(true);
                currentActions.Add(actionToUse);
            }
            
            cancelAction.gameObject.SetActive(true);
            currentActions.Add(cancelAction);
            
            //LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
        }

        public void SelectAction(ActionData actionData)
        {
            var actionToUse = availableActions.Find(m => m.GetActionType() == actionData.actionType);
            descriptionLabel.text = actionData.description;
        }
    }
}