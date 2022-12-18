using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    private void Start()
    {
        CustomGrid.instance.SetWalkable(CustomGrid.instance.NodeFromWorldPoint(transform.position),false);
    }
}
