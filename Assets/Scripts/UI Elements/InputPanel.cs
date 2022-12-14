﻿using UnityEngine;
using UnityEngine.EventSystems;
public class InputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler
{
    public static InputPanel instance;
    [System.NonSerialized] public GameObject tutorial;
    [System.NonSerialized] public float horizontal;
    Vector2 _lastPosition = Vector2.zero;
    public static float valX;
    private void Awake()
    {
        instance = this;
        //tutorial = transform.GetChild(0).gameObject;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (LevelManager.gamestate == GameState.BeforeStart)
        {
            _lastPosition = eventData.position;
            PlayArea.instance.GameStart();
        }
        else if(LevelManager.gamestate == GameState.Normal)
        {
            _lastPosition = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (LevelManager.gamestate == GameState.Normal)
        {
            Vector2 direction = eventData.position - _lastPosition;
            horizontal = direction.x * 2 / Screen.width;
            valX = Mathf.Lerp(valX, horizontal, 0.4f);
            _lastPosition = eventData.position;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        horizontal = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (LevelManager.gamestate == GameState.Normal)
        {
            _lastPosition = eventData.position;
        }
    }
}