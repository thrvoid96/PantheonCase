using UnityEngine;

/// <summary>
/// Interactable data model.
/// </summary>
public class InteractableData : ScriptableObject
{
    // Interactable name
    public string name = "This is a placeholder.";
    
    // Interactable description
    public string description = "This is a placeholder.";

    // Interactable image
    //public Sprite image;


    public InteractableData(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}