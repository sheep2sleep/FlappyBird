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

    public float UltCD = 10f;
    private float fireTimer3 = 0;

    private Missile missile = null;

    /// <summary>
    /// Boss��ʼ����Ϊ
    /// </summary>
    public override void OnStart()
    {
        Fly();
        StartCoroutine(Enter());
    }

    /// <summary>
    /// Boss����Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Enter()
    {
        this.transform.position = new Vector3(18, 0.6f, 0);
        yield return MoveTo(new Vector3(6.5f, 0.6f, 0));
        yield return Attack();
    }

    /// <summary>
    /// Boss�ƶ���ָ��λ��Э��
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private IEnumerator MoveTo(Vector3 pos)
    {
        //ʹ��while��ʵ�ֻ����ƶ�
        while (true)
        {
            Vector3 dir = (pos - this.transform.position);
            if (dir.magnitude < 0.1)
            {
                break;
            }
            this.transform.position += dir.normalized * speed * Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Boss����Э��
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        while (true)
        {
            //������̨
            fireTimer2 += Time.deltaTime;
            Fire();
            Fire2();
            //���䵼��
            fireTimer3 += Time.deltaTime;
            if (fireTimer3 > UltCD)
            {
                yield return UltraAttack();
                fireTimer3 = 0;
            }
            yield return null;
        }
    }

    /// <summary>
    /// ���䵼��ʱ������
    /// </summary>
    /// <returns></returns>
    private IEnumerator UltraAttack()
    {
        yield return MoveTo(new Vector3(6.5f, 5f, 0));
        yield return FireMissile();
        yield return MoveTo(new Vector3(6.5f, 0.6f, 0));
    }

    /// <summary>
    /// ���䵼��
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMissile()
    {
        ani.SetTrigger("Skill");
        yield return new WaitForSeconds(3f);        
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
