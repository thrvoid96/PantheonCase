using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controller responsible for game phase.
/// </summary>
public class InformationController : SubController<UIGameRoot>
{
    // Reference to current interactable data.
    private InteractableData interactableData;

    // Reference to current action datas.
    private List<ActionData> actionDatas;
    
    public override void EngageController()
    {
        // interactableData = new InteractableData
        // {
        //     name =  "sus",
        //     description = "amogus"
        // };
        base.EngageController();
        
        // Attaching UI events.

        // for (int i = 0; i < actionDatas.Count; i++)
        // {
        //     ui.GameView.OnFinishClicked += ;
        // }
       
    }

    public override void DisengageController()
    {
        base.DisengageController();
        
        // Detaching UI events.
        //ui.GameView.OnFinishClicked += FinishGame;
    }

    // /// <summary>
    // /// Unity method called each frame as game object is enabled.
    // /// </summary>
    // private void Update()
    // {
    //     // Increasing time value.
    //     actionData.gameTime += Time.deltaTime;
    //     // Displaying current game time.
    //     ui.GameView.UpdateTime(gameData.gameTime);
    // }

    /// <summary>
    /// Handling UI Action Button Click.
    /// </summary>
    private void SetupActions()
    {
        // Assigning random score.
        //interactableData.gameScore = Mathf.CeilToInt(gameData.gameTime * Random.Range(0.0f, 10.0f));
        // Saving GameData in DataStorage.
        DataStorage.Instance.SaveData(Keys.ACTION_DATA_KEY, interactableData);

        // Chaning controller to Game Over Controller
        root.ChangeController(RootController.ControllerTypeEnum.GameOver);
    }

    // /// <summary>
    // /// Handling UI Menu Button Click.
    // /// </summary>
    // private void GoToMenu()
    // {
    //     // Changing controller to Menu Controller.
    //     root.ChangeController(RootController.ControllerTypeEnum.Menu);
    // }
}