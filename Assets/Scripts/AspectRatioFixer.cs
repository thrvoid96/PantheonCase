using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioFixer : MonoBehaviour
{
    private void Awake()
    {
        ScreenResolutionArranger();
    }

    void ScreenResolutionArranger()
    {
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        float screenRatio = ((float)Screen.width / Screen.height);
        Debug.LogError(screenRatio);

    }
}
