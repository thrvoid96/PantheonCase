using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    private void Start()
    {
        CustomGrid.Instance.SetWalkable(CustomGrid.Instance.NodeFromWorldPoint(transform.position),false);
    }
}
