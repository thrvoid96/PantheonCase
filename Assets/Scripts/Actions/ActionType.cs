using System;

namespace Actions
{
    /// <summary>
    /// This is used for actions initialization in ActionsView.
    /// Put new enums at the bottom for not distrupting other ones.
    /// </summary>
    [Serializable]
    public enum ActionType
    {
        Cancel,
        Placeholder,
        GoTo,
        Place,
        ChangeSpawnPoint,
        SpawnSoldier,
        ProducePower
        //<= Here.
    }
}