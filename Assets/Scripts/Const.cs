using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 角色阵营
/// </summary>
public enum SIDE
{
    NONE = 0,
    PLAYER,
    ENEMY,
}


/// <summary>
/// 敌人类型
/// </summary>
public enum ENEMY_TYPE
{
    NORMAL_ENEMY,

    //摇摆的敌人
    SWING_ENEMY,

    BOSS,
}

/// <summary>
/// 游戏状态
/// </summary>
public enum GAME_STATUS
{
    Ready,
    InGame,
    GameOver
}