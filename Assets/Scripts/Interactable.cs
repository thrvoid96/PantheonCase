using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private InteractableData interactableData;
    [SerializeField] protected List<SpriteRenderer> gridParts;
    [SerializeField] protected List<BaseAction> actions;
    [SerializeField] protected Collider2D hitCollider;
    private bool placedOnGrid;
    public bool currentlyDoingAction { get; protected set; }

    public List<BaseAction> getActions => actions;

    public InteractableData getInteractableData => interactableData;
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
    
    public void UpdateGridPartColors(bool placementComplete)
    {
        if (placementComplete)
        {
            foreach (SpriteRenderer gridSprite in gridParts)
            {
                var color = gridSprite.color;
                color.a = 0f;
                gridSprite.sortingOrder = 0;
                gridSprite.color = color;
            }
            return;
        }

        foreach (SpriteRenderer gridSprite in gridParts)
        {
            var color = gridSprite.color;
            
            if (CustomGrid.Instance.NodeFromWorldPoint(gridSprite.transform.position).walkable)
            {
                gridSprite.sortingOrder = 0;
                color = Color.green;
            }
            else
            {
                gridSprite.sortingOrder = 2;
                color = Color.red;
            }

            color.a = 1f;
            gridSprite.color = color;
            
        }
    }
    /// <summary>
    /// Returns true if a path has been found.
    /// </summary>
    public virtual bool TryPathfinding(bool startFollow,Vector3 targetPos)
    {
        return false;
    }
    
    public virtual void ClearPath()
    {
        
    }

    public virtual bool TryChangeSpawnPoint()
    {
        return false;
    }

    public virtual GameObject GetSpawnPointObj()
    {
        return null;
    }
    
    public virtual bool TryPlacement()
    {
        return false;
    }

    public virtual void ResetLineRendererToFirst()
    {
        
    }

    public virtual void OnMouseDown()
    {
        PlayerController.Instance.SelectNewInteractable(this);
    }
}
