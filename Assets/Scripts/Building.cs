using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Interactable
{
    [SerializeField] protected List<GameObject> buildingParts;
    
    /// <summary>
    /// Returns true if the placement was successfull.
    /// </summary>
    public override bool TryPlacement()
    {
        base.TryPlacement();
        
        List<Node> tempList = new List<Node>();
        var returnValue = false;
        foreach (var part in buildingParts)
        {
            var node = CustomGrid.Instance.NodeFromWorldPoint(part.transform.position);
            if (node.walkable)
            {
                tempList.Add(node);
            }
        }

        if (tempList.Count == buildingParts.Count)
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

    public override void SetupLineRenderer(Vector3 endPos)
    {
        base.SetupLineRenderer(endPos);
    }
}
