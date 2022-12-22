using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private InteractableData interactableData;
    [SerializeField] protected List<ActionData> possibleActions;
    [SerializeField] protected Collider hitCollider;
    private bool placedOnGrid;
    public bool currentlyDoingAction { get; protected set; }

    public InteractableData getInteractableData => interactableData;
    public List<ActionData> getActionsData => possibleActions;
    public bool CheckIfAlreadyPlaced()
    {
        return placedOnGrid;
    }
    
    /// <summary>
    /// Method to set if the interactable has been put on the grid or not.
    /// </summary>
    public void SetPlacementBool(bool value)
    {
        placedOnGrid = value;
    }
    
    public void CanBeInteracted(bool value)
    {
        hitCollider.enabled = value;
    }

    public virtual void ChangeActions()
    {
        
    }
    
    public virtual void DoPathfinding(bool startFollow,Vector3 targetPos)
    {
        
    }

    public virtual void SetupLineRenderer(Vector3 endPos)
    {
        
    }

    public virtual void ClearPath()
    {
        
    }

    public virtual void ChangeSpawnPoint(Node nodeToTest)
    {
        
    }

    public virtual GameObject GetSpawnPointObj()
    {
        return null;
    }

    public virtual void ChangeSprite(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    
    public virtual bool TryPlacement()
    {
        return false;
    }

    public virtual void OnMouseDown()
    {
        PlayerController.Instance.SelectNewInteractable(this);
    }
}
