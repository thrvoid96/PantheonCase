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
            Actions,
            GameOver
        }

        // References to the subcontrollers.
        [Header("Controllers")]
        [SerializeField]
        private InformationController infoController;
        [SerializeField]
        private ActionsController actionsController;
        /*
        [SerializeField] 
        private GameOverController gameOverController;*/

        /// <summary>
        /// Unity method called on first frame.
        /// </summary>
        private void Start()
        {
            infoController.root = this;
            actionsController.root = this;
            //gameOverController.root = this;

            //ChangeController(ControllerTypeEnum.Information);
        }

        /// <summary>
        /// Method used by subcontrollers to change game phase.
        /// Closes every other controller after used
        /// </summary>
        /// <param name="controller">Controller type.</param>
        public void ChangeController(ControllerTypeEnum controller)
        {
            // Reseting subcontrollers.
            DisengageAllControllers();

            // Enabling subcontroller based on type.
            switch (controller)
            {
                case ControllerTypeEnum.Information:
                    infoController.EngageController();
                    break;
                case ControllerTypeEnum.Actions:
                    actionsController.EngageController();
                    break;
                case ControllerTypeEnum.GameOver:
                    //gameOverController.EngageController();
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
        
        /// <summary>
        /// Method used to disable all attached subcontrollers.
        /// </summary>
        private void DisengageAllControllers()
        {
            infoController.DisengageController();
            //gameController.DisengageController();
            //gameOverController.DisengageController();
        }
    
    
    }
}