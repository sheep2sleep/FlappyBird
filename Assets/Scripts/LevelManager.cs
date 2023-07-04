using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public List<Level> levels;
    public int currentLevelId = 1;
    public Level level;

    public UnitManager unitManager;
    public Player currentPlayer;

    /// <summary>
    /// °ÑlevelÊµÀý»¯
    /// </summary>
    /// <param name="levelID"></param>
    public void LoadLevel(int levelID)
    {
        this.level = Instantiate<Level>(levels[levelID - 1]);
        this.level.unitManager = this.unitManager;
        this.level.currentPlayer = this.currentPlayer;
    }
}