using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Interactable
{
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
            
            returnValue = true;
        }

        SetPlacementBool(returnValue);
        CanBeInteracted(returnValue);
        return returnValue;
    }
}
