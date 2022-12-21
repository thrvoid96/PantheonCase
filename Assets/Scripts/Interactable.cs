using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    [SerializeField] private InteractableData interactableData;
    [SerializeField] private List<ActionData> possibleActions;
    [SerializeField] protected Collider hitCollider;

    public InteractableData getInteractableData => interactableData;
    public List<ActionData> getActionsData => possibleActions;
    
    public virtual void DoPathfinding(bool startFollow,Vector3 targetPos)
    {
        
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
