using UnityEngine;

/// <summary>
/// Interactable data model.
/// </summary>
/// 
[CreateAssetMenu(fileName = "InteractableData", menuName = "Datas/InteractableData", order = 1)]
public class InteractableData : ScriptableObject
{
    // Interactable name
    public string objName = "This is a placeholder.";
    
    // Interactable description
    public string description = "This is a placeholder.";

    // Interactable image
    public Sprite sprite;
    
    /// <summary>
    /// Constructor.
    /// <param name="objName">Name of the object.</param>
    /// <param name="description">Description of the object.</param>
    /// <param name="sprite">Sprite of the object.</param>
    /// </summary>
    public InteractableData(string objName, string description, Sprite sprite)
    {
        this.objName = objName;
        this.description = description;
        this.sprite = sprite;
    }
}