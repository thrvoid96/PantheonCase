using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioFixer : MonoBehaviour
{
    public CustomLayoutGroup customLayoutGroup;
    public float layoutGroupRatio, cameraRatio;

    private float widthDiff;
    private void Awake()
    {
        ScreenResolutionArranger();
    }

    private void ScreenResolutionArranger()
    {
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        
        widthDiff = 1920f - Screen.width;
        
        LayoutGroupFix();
        CameraDistanceFix();
    }

    private void LayoutGroupFix()
    {
        var newOffset = (widthDiff / layoutGroupRatio) * -1f;
        customLayoutGroup.Offset.x = newOffset;
    }

    private void CameraDistanceFix()
    {
        CameraMain.Instance.mainCam.orthographicSize += (widthDiff / cameraRatio);
    }
}
