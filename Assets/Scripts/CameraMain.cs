using UnityEngine;

public class CameraMain : PersistentLazySingleton<CameraMain>
{
    //[SerializeField] private LayerMask inputLayerMask;

    [HideInInspector] public Camera mainCam;
    
    void Start()
    {
        mainCam = GetComponent<Camera>();
        //mainCam.eventMask = inputLayerMask;
    }
    
}
