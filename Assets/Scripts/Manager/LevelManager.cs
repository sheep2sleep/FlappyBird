using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoSingleton<LevelManager>
{
    public List<Level> levels;
    public int currentLevelId = 1;
    public Level level;

    /// <summary>
    /// °ÑlevelÊµÀý»¯
    /// </summary>
    /// <param name="levelID"></param>
    public void LoadLevel(int levelID)
    {
        this.level = Instantiate<Level>(levels[levelID - 1]);
    }
}