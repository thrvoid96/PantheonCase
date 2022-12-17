using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Coroutine _coroutine;
    private Unit selectedSoldier;

    private void Awake()
    {
        instance = this;
    }

    public void SelectNewSoldier(Unit soldier)
    {
        if (selectedSoldier != null)
        {
            selectedSoldier.ChangeColor(Color.white);
        }
        
        selectedSoldier = soldier;
        selectedSoldier.ChangeColor(Color.green);

        if (_coroutine != null)
        {
            StopCoroutine(nameof(WaitForPlayerInput));
        }
        _coroutine = StartCoroutine(nameof(WaitForPlayerInput));
    }

    IEnumerator WaitForPlayerInput()
    {
        while (true)
        {
            Ray ray = CameraMain.instance.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid"))) 
            {
                if (Input.GetMouseButtonDown(1))
                {
                    selectedSoldier.GoForPath(true,hit.point);
                    selectedSoldier.ChangeColor(Color.white);
                    selectedSoldier = null;
                    yield break;
                }
                else
                {
                    selectedSoldier.GoForPath(false,hit.point); 
                    yield return null;
                }
            }   
             
            yield return null;
        }
    }
    
    // public void OnDrawGizmos() {
    //     
    //     if (selectedSoldier!=null)
    //     {
    //         Gizmos.color = Color.black;
    //         Ray ray = CameraMain.instance.mainCam.ScreenPointToRay (Input.mousePosition);
    //         RaycastHit hit;
    //         if (Physics.Raycast (ray, out hit, Mathf.Infinity)) 
    //         {
    //             Gizmos.DrawCube(CustomGrid.instance.NodeFromWorldPoint(hit.point).worldPosition, Vector3.one);
    //         }   
    //     }
    // }
}
