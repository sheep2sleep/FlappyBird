using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject missileTemplate;

    public Transform firePoint2;
    public Transform firePoint3;
    public Transform battery;

    public Unit target;

    private Missile missile = null;

    /// <summary>
    /// Boss初始化行为
    /// </summary>
    public override void OnStart()
    {
        StartCoroutine(FireMissile());
    }

    /// <summary>
    /// 临时的发射导弹方法
    /// </summary>
    /// <returns></returns>
    IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(5f);
        ani.SetTrigger("Skill");
    }

    /// <summary>
    /// Boss每帧的行为
    /// </summary>
    public override void OnUpdate()
    {

    }

    /// <summary>
    /// 导弹加载
    /// </summary>
    public void OnMissileLoad()
    {
        //在firePoint3下创建一个GameObject
        GameObject go = Instantiate(missileTemplate, firePoint3);
        //获取导弹组件并设置导弹目标
        missile = go.GetComponent<Missile>();
        missile.target = target.transform;
    }

    /// <summary>
    /// 导弹发射
    /// </summary>
    public void OnMissileLaunch()
    {
        if (missile == null)
            return;
        missile.transform.SetParent(null);
        missile.Launch();
    }
}
