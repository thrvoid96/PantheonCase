using System.Collections.Generic;
using UnityEngine;

namespace MVC.Controllers
{
    /// <summary>
    /// Root controller responsible for changing game phases with SubControllers.
    /// </summary>
    public class RootController : PersistentLazySingleton<RootController>
    {
        // SubControllers types.
        public enum ControllerTypeEnum
        {
            Information,
            Actions
        }

        // References to the subcontrollers.
        [Header("Controllers")]
        [SerializeField]
        private InformationController infoController;
        [SerializeField]
        private ActionsController actionsController;

        /// <summary>
        /// Unity method called on first frame.
        /// </summary>
        private void Start()
        {
            infoController.root = this;
            actionsController.root = this;
            
            DisengageController(ControllerTypeEnum.Information);
        }

        /// <summary>
        /// Method used by subcontrollers to change UI state.
        /// </summary>
        /// <param name="controller">Controller type.</param>
        public void EngageController(ControllerTypeEnum controller)
        {
            // Enabling subcontroller based on type.
            switch (controller)
            {
                case ControllerTypeEnum.Information:
                    infoController.EngageController();
                    break;
                case ControllerTypeEnum.Actions:
                    actionsController.EngageController();
                    break;
                default:
                    break;
            }
        }
        
        /// <summary>
        /// Method used by subcontrollers to change UI state.
        /// </summary>
        /// <param name="controller">Controller type.</param>
        public void DisengageController(ControllerTypeEnum controller)
        {
            // Enabling subcontroller based on type.
            switch (controller)
            {
                case ControllerTypeEnum.Information:
                    infoController.DisengageController();
                    break;
                case ControllerTypeEnum.Actions:
                    actionsController.DisengageController();
                    break;
                default:
                    break;
            }
        }

        public void SetupInfoPanel(InteractableData interactableData)
        {
            infoController.SetupInfoView(interactableData);
        }
        
        public void SetupActionsPanel(List<ActionData> actionDatas)
        {
            actionsController.SetupActionsView(actionDatas);
        }

        public void TriggerCurrentAction()
        {
            actionsController.TriggerCurrentAction();
        }
    }
}