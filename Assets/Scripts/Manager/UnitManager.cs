using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<Enemy> enemies = new List<Enemy>();

    /// <summary>
    /// 清除所有敌人
    /// </summary>
    public void Clear()
    {
        this.enemies.Clear();
    }

    /// <summary>
    /// 创建指定类型的敌人
    /// </summary>
    /// <param name="templates"></param>
    /// <returns></returns>
    public Enemy CreateEnemy(GameObject templates)
    {
        if (templates == null)
            return null;

        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        this.enemies.Add(p);

        return p;
    }
}
