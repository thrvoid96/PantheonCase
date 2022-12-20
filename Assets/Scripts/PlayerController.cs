using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Coroutine _coroutine1,_coroutine2;
    private Unit selectedUnit;
    private Building selectedBuilding;

    private void Awake()
    {
        instance = this;
    }

    

    public void SelectNewUnit(Unit unit)
    {
        if (selectedUnit != null)
        {
            selectedUnit.ChangeColor(Color.magenta);
        }
        
        selectedUnit = unit;
        selectedUnit.ChangeColor(Color.green);

        if (_coroutine1 != null)
        {
            StopCoroutine(nameof(WaitForMoveInput));
        }
        _coroutine1 = StartCoroutine(nameof(WaitForMoveInput));
    }
    
    public void SelectNewBuilding(Building building)
    {
        selectedBuilding = building;
        
        if (_coroutine2 != null)
        {
            StopCoroutine(nameof(WaitForPlaceInput));
        }
        _coroutine2 = StartCoroutine(nameof(WaitForPlaceInput));
    }
    

    IEnumerator WaitForPlaceInput()
    {
        while (true)
        {
            Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid")))
            {
                var hoveringGrid = CustomGrid.Instance.NodeFromWorldPoint(hit.point);
                selectedBuilding.transform.position = hoveringGrid.worldPosition;
                if (Input.GetMouseButtonDown(1))
                {
                    var placementComplete = selectedBuilding.TryPlaceBuilding();
                    if (placementComplete)
                    {
                        selectedBuilding = null;
                        yield break;
                    }
                }
            }
            else
            {
                
                selectedBuilding.transform.position = CameraMain.Instance.mainCam.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 10f);
            }
             
            yield return null;
        }
    }
    
    IEnumerator WaitForMoveInput()
    {
        while (true)
        {
            Ray ray = CameraMain.Instance.mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, Mathf.Infinity,LayerMask.GetMask("Grid"))) 
            {
                if (Input.GetMouseButtonDown(1))
                {
                    selectedUnit.GoForPath(true,hit.point);
                    selectedUnit.ChangeColor(Color.magenta);
                    selectedUnit = null;
                    yield break;
                }
                else
                {
                    selectedUnit.GoForPath(false,hit.point); 
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
