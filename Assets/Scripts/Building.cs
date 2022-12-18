using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Interactable
{
    public void PlaceBuilding(Vector3 position)
    {
        transform.position = position;
    }
}
