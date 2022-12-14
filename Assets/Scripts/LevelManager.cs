using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static GameState gamestate;
    [NonSerialized] public LevelAssetCreate levelAsset;
    

    private void Awake()
    {
        gamestate = GameState.BeforeStart;
        instance = this;
        SetValues();
    }
    
    //--------------------------------------------------------------------------//
    void SetValues()
    {
        levelAsset = Resources.Load<LevelAssetCreate>("Scriptables/LevelAsset");
    }
    
    public void Victory(float delay = 0.9f)
    {
        Debug.Log("VICTORY");
        gamestate = GameState.Victory;
    }

    //-------------------------------------------------------------------//
    public void Fail()
    {
        Debug.Log("FAILED");
        gamestate = GameState.Failed;
    }

    //----------------------------------------------------------------------------------------//
    public int FindTotalScore()
    {
        int totalScore = GameManager.Level * 10 + 50;
        return totalScore;
    }
}
