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

    public float fireRate2 = 10f;
    private float fireTimer2 = 0;

    private Missile missile = null;

    /// <summary>
    /// Boss��ʼ����Ϊ
    /// </summary>
    public override void OnStart()
    {
        Fly();
        StartCoroutine(FireMissile());
        StartCoroutine(Attack());
    }

    /// <summary>
    /// Boss����Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        while (true)
        {
            fireTimer2 += Time.deltaTime;
            //Fire();
            Fire2();

            //fireTimer3 += Time.deltaTime;
            //if (fireTimer3 > UltCD)
            //{
            //    yield return UltraAttack();
            //    fireTimer3 = 0;
            //}
            yield return null;
        }
    }

    /// <summary>
    /// ��ʱ���䵼��
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(3f);
        ani.SetTrigger("Skill");
    }

    /// <summary>
    /// ��ʱ������̨�ӵ�
    /// </summary>
    private void Fire2()
    {
        if (fireTimer2 > 1f / fireRate2)
        {
            GameObject go = Instantiate(bulletTemplate, firePoint2.position, battery.rotation);
            Element bullent = go.GetComponent<Element>();
            bullent.direction = (target.transform.position - firePoint2.position).normalized;
            fireTimer2 = 0f;
        }
    }

    /// <summary>
    /// Bossÿ֡����Ϊ
    /// </summary>
    public override void OnUpdate()
    {
        //��̨��ת
        if (target != null)
        {
            Vector3 dir = (target.transform.position - battery.position).normalized;
            battery.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        }
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
