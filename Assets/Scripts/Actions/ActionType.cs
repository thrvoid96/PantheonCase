using System;

namespace Actions
{
    /// <summary>
    /// This is used for actions initialization in ActionsView.
    /// Do not put enums over "Cancel" or "Placeholder"
    /// </summary>
    [Serializable]
    public enum ActionType
    {
        Cancel,
        Placeholder,
        //<= Can put your new enums starting from here.
        Follow,
        Place,
        ChangeSpawnPoint,
        SpawnSoldier,
        ProducePower
        //<= Or here.
    }
}