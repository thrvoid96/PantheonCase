using UnityEngine;

/// <summary>
/// Interactable data model.
/// </summary>
/// 
[CreateAssetMenu(fileName = "InteractableData", menuName = "Datas/InteractableData", order = 1)]
public class InteractableData : ScriptableObject
{
    // Interactable name
    public string name = "This is a placeholder.";
    
    // Interactable description
    public string description = "This is a placeholder.";

    // Interactable image
    public Sprite sprite;
    
    public InteractableData(string name, string description, Sprite sprite)
    {
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }
}