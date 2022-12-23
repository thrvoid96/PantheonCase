using UnityEngine;
using UnityEngine.U2D;

public class CameraMain : Singleton<CameraMain>
{
    //[SerializeField] private LayerMask inputLayerMask;

    [HideInInspector] public Camera mainCam;
    [HideInInspector] public PixelPerfectCamera pixelPerfectCam;
    private void Awake()
    {
        mainCam = GetComponent<Camera>();
        pixelPerfectCam = GetComponent<PixelPerfectCamera>();
        //mainCam.eventMask = inputLayerMask;
    }
    
}
