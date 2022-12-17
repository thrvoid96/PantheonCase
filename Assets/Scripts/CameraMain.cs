using UnityEngine;

public class CameraMain : MonoBehaviour
{
    public static CameraMain instance;
    //[SerializeField] private LayerMask inputLayerMask;

    [HideInInspector] public Camera mainCam;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mainCam = GetComponent<Camera>();
        //mainCam.eventMask = inputLayerMask;
    }
    
}
