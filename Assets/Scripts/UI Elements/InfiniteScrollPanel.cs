using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScrollPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    #region Private Members

    /// <summary>
    /// The ScrollContent component that belongs to the scroll content GameObject.
    /// </summary>
    [SerializeField]
    private CustomLayoutGroup customLayoutContent;

    /// <summary>
    /// How far the items will travel outside of the scroll view before being repositioned.
    /// </summary>
    [SerializeField]
    private float outOfBoundsThreshold;

    /// <summary>
    /// The ScrollRect component for this GameObject.
    /// </summary>
    private ScrollRect scrollRect;

    /// <summary>
    /// The last position where the user has dragged.
    /// </summary>
    private Vector2 lastDragPosition;

    /// <summary>
    /// Is the user dragging in the positive axis or the negative axis?
    /// </summary>
    private bool positiveDrag;

    #endregion

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.vertical = customLayoutContent.Vertical;
        scrollRect.horizontal = customLayoutContent.Horizontal;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    /// <summary>
    /// Called when the user starts to drag the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Called while the user is dragging the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (customLayoutContent.Vertical)
        {
            positiveDrag = eventData.position.y > lastDragPosition.y;
        }
        else if (customLayoutContent.Horizontal)
        {
            positiveDrag = eventData.position.x > lastDragPosition.x;
        }

        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Called when the user starts to scroll with their mouse wheel in the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the scroll event.</param>
    public void OnScroll(PointerEventData eventData)
    {
        if (customLayoutContent.Vertical)
        {
            positiveDrag = eventData.scrollDelta.y > 0;
        }
        else
        {
            // Scrolling up on the mouse wheel is considered a negative scroll, but I defined
            // scrolling downwards (scrolls right in a horizontal view) as the positive direciton,
            // so I check if the if scrollDelta.y is less than zero to check for a positive drag.
            positiveDrag = eventData.scrollDelta.y < 0;
        }
    }

    /// <summary>
    /// Called when the user is dragging/scrolling the scroll view.
    /// </summary>
    public void OnViewScroll()
    {
        if (customLayoutContent.Vertical)
        {
            HandleVerticalScroll();
        }
        else
        {
            HandleHorizontalScroll();
        }
    }

    /// <summary>
    /// Called if the scroll view is oriented vertically.
    /// </summary>
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Transform secondEndItem = null;
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            secondEndItem = scrollRect.content.GetChild(1);
            
        }
        else
        {
            secondEndItem = scrollRect.content.GetChild(endItemIndex-1);
        }
        var posDiff = endItem.position.y - secondEndItem.position.y;
        newPos.y = endItem.position.y + posDiff;
        
        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    /// <summary>
    /// Called if the scroll view is oriented horizontally.
    /// </summary>
    private void HandleHorizontalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);
        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.x = endItem.position.x - customLayoutContent.ChildWidth * 1.5f + customLayoutContent.ItemSpacing;
        }
        else
        {
            newPos.x = endItem.position.x + customLayoutContent.ChildWidth * 1.5f - customLayoutContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    /// <summary>
    /// Checks if an item has the reached the out of bounds threshold for the scroll view.
    /// </summary>
    /// <param name="item">The item to be checked.</param>
    /// <returns>True if the item has reached the threshold for either ends of the scroll view, false otherwise.</returns>
    private bool ReachedThreshold(Transform item)
    {
        if (customLayoutContent.Vertical)
        {
            float posYThreshold = transform.position.y + customLayoutContent.Height * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - customLayoutContent.Height * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.y - customLayoutContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + customLayoutContent.ChildWidth * 0.5f < negYThreshold;
        }
        else
        {
            float posXThreshold = transform.position.x + customLayoutContent.Width * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - customLayoutContent.Width * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.x - customLayoutContent.ChildWidth * 0.5f > posXThreshold :
                item.position.x + customLayoutContent.ChildWidth * 0.5f < negXThreshold;
        }
    }
}
