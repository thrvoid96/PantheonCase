using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    public Node soldierSpawnNode { get; private set; }

    public override bool TryPlacement()
    {
        SetupSoldierSpawnPoint();
        
        return base.TryPlacement();
    }

    private void SetupSoldierSpawnPoint()
    {
        
    }
}
