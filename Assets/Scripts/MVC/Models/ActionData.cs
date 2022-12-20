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
    public string name = "This is a placeholder.";
    
    // Action description
    public string description = "This is a placeholder.";
    
    // Action image
    public Sprite sprite;
    
    // Action cooldown
    public float cooldown = 0.1f;
    
    public ActionData(string name, string description, Sprite sprite, float cooldown)
    {
        this.name = name;
        this.description = description;
        this.sprite = sprite;
        this.cooldown = cooldown;
    }
}