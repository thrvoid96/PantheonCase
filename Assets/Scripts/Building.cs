using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public abstract class Building : Interactable
{
    [SerializeField] private List<BaseAction> nextActions;
    
    /// <summary>
    /// Returns true if the placement was successfull.
    /// </summary>
    public override bool TryPlacement()
    {
        base.TryPlacement();
        
        List<Node> tempList = new List<Node>();
        var returnValue = false;
        foreach (var part in gridParts)
        {
            var node = CustomGrid.Instance.NodeFromWorldPoint(part.transform.position);
            if (node.walkable)
            {
                tempList.Add(node);
            }
        }

        if (tempList.Count == gridParts.Count)
        {
            foreach (Node node in tempList)
            {
                node.walkable = false;
            }
            
            ChangeActions();
            returnValue = true;
        }

        SetPlacementBool(returnValue);
        CanBeInteracted(returnValue);
        return returnValue;
    }
    
    public override void ChangeActions()
    {
        actions = new List<BaseAction>(nextActions);
        nextActions.Clear();
    }
}
