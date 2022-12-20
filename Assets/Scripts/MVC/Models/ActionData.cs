using UnityEngine;

/// <summary>
/// Action data model.
/// </summary>
public class ActionData : ScriptableObject
{
    //Action type
    public ActionType actionType = ActionType.Placeholder;
    
    // Action name
    public string name = "This is a placeholder.";
    
    // Action description
    public string description = "This is a placeholder.";
    
    // Action image
    public Sprite image;
    
    // Action cooldown
    public float cooldown = 0.1f;
    
    public ActionData(string name, string description, Sprite image, float cooldown)
    {
        this.name = name;
        this.description = description;
        this.image = image;
        this.cooldown = cooldown;
    }
}