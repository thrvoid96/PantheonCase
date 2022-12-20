using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceBuilding : MonoBehaviour
{
    public string buildingName;
    public void CreateBuilding()
    {
        var spawnedObject = ObjectPool.Instance.SpawnFromPool(buildingName);
        var interactable = spawnedObject.GetComponent<Interactable>();
        PlayerController.Instance.SelectNewInteractable(interactable);
    }
}
