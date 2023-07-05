using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<Enemy> enemies = new List<Enemy>();

    /// <summary>
    /// ������е���
    /// </summary>
    public void Clear()
    {
        this.enemies.Clear();
    }

    /// <summary>
    /// ����ָ�����͵ĵ���
    /// </summary>
    /// <param name="templates"></param>
    /// <returns></returns>
    public Enemy CreateEnemy(GameObject templates)
    {
        if (templates == null)
            return null;

        GameObject obj = Instantiate(templates, this.transform);
        Enemy p = obj.GetComponent<Enemy>();
        p.OnDeath += Enemy_OnDeath;
        this.enemies.Add(p);

        return p;
    }

    /// <summary>
    /// ���������ӷ�
    /// </summary>
    /// <param name="sender"></param>
    private void Enemy_OnDeath(Unit sender)
    {
        Game.Instance.Score += sender.dieScore;
    }
}
