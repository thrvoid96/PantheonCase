using System;


/// <summary>
/// This is used for actions initialization in ActionsView.
/// Do not put enums under "Cancel" or "Placeholder"
/// </summary>
[Serializable]
public enum ActionType
{
    Follow,
    //<= Put your new enums starting from here.
    Cancel,
    Placeholder
}