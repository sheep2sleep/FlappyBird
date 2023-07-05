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
    /// Boss初始化行为
    /// </summary>
    public override void OnStart()
    {
        Fly();
        StartCoroutine(Enter());
    }

    /// <summary>
    /// Boss到达协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator Enter()
    {
        this.transform.position = new Vector3(18, 0.6f, 0);
        yield return MoveTo(new Vector3(6.5f, 0.6f, 0));
        yield return Attack();
    }

    /// <summary>
    /// Boss移动到指定位置协程
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private IEnumerator MoveTo(Vector3 pos)
    {
        //使用while来实现缓慢移动
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
    /// Boss攻击协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        while (true)
        {
            //发射炮台
            fireTimer2 += Time.deltaTime;
            Fire();
            Fire2();
            //发射导弹
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
    /// 发射导弹时的连招
    /// </summary>
    /// <returns></returns>
    private IEnumerator UltraAttack()
    {
        yield return MoveTo(new Vector3(6.5f, 5f, 0));
        yield return FireMissile();
        yield return MoveTo(new Vector3(6.5f, 0.6f, 0));
    }

    /// <summary>
    /// 发射导弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireMissile()
    {
        ani.SetTrigger("Skill");
        yield return new WaitForSeconds(3f);        
    }

    /// <summary>
    /// 定时发射炮台子弹
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
    /// Boss每帧的行为
    /// </summary>
    public override void OnUpdate()
    {
        //炮台旋转
        if (target != null)
        {
            Vector3 dir = (target.transform.position - battery.position).normalized;
            battery.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
        }
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
