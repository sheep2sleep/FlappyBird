using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SIDE
{
    NONE = 0,
    PLAYER,
    ENEMY,
}

public enum ENEMY_TYPE
{
    NORMAL_ENEMY,

    //摇摆的敌人
    SWING_ENEMY,

    BOSS,
}
