using Actions;
using UnityEngine;

/// <summary>
/// Action data model.
/// </summary>
[CreateAssetMenu(fileName = "ActionData", menuName = "Datas/ActionData", order = 0)]
public class ActionData : ScriptableObject
{
    //Action type
    public ActionType actionType = ActionType.Placeholder;
    
    // Action name
    public string objName = "This is a placeholder.";
    
    // Action description
    public string description = "This is a placeholder.";
    
    // Action image
    public Sprite sprite;
    
    // Action cooldown
    public float cooldown = 0.1f;
    
    // Action cost
    public int cost = 1;
    
    /// <summary>
    /// Constructor.
    /// <param name="objName">Name of the action.</param>
    /// <param name="description">Description of the action.</param>
    /// <param name="sprite">Sprite of the action.</param>
    /// <param name="cooldown">Cooldown of the object.</param>
    /// </summary>
    public ActionData(string objName, string description, Sprite sprite, float cooldown, int cost)
    {
        this.objName = objName;
        this.description = description;
        this.sprite = sprite;
        this.cooldown = cooldown;
        this.cost = cost;
    }
}