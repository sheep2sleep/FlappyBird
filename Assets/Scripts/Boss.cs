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
    /// Boss��ʼ����Ϊ
    /// </summary>
    public override void OnStart()
    {
        StartCoroutine(FireMissile());
    }

    /// <summary>
    /// ��ʱ�ķ��䵼������
    /// </summary>
    /// <returns></returns>
    IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(5f);
        ani.SetTrigger("Skill");
    }

    /// <summary>
    /// Bossÿ֡����Ϊ
    /// </summary>
    public override void OnUpdate()
    {

    }

    /// <summary>
    /// ��������
    /// </summary>
    public void OnMissileLoad()
    {
        //��firePoint3�´���һ��GameObject
        GameObject go = Instantiate(missileTemplate, firePoint3);
        //��ȡ������������õ���Ŀ��
        missile = go.GetComponent<Missile>();
        missile.target = target.transform;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void OnMissileLaunch()
    {
        if (missile == null)
            return;
        missile.transform.SetParent(null);
        missile.Launch();
    }
}
