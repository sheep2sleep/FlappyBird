using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Element
{
    //导弹跟踪目标
    public Transform target;
    private bool running = false;

    public GameObject fxExpold;

    private float existTimer = 0f;

    /// <summary>
    /// 导弹开始
    /// </summary>
    public override void OnStart()
    {
        //相比于其他武器，导弹自动销毁得加爆炸动画
        //所以不使用原start中的Destory，而是另外加计时器
    }

    /// <summary>
    /// 导弹每帧更新
    /// </summary>
    public override void OnUpdate()
    {
        if (!running)
            return;

        if (target != null)
        {
            //自动爆炸
            existTimer += Time.deltaTime;
            if (existTimer > destroyTime)
            {
                Explod();
                existTimer = 0;
            }
            //目标位置距离自己位置的方向
            Vector3 dir = (target.position - this.transform.position);
            if (dir.magnitude < 0.1)
            {
                Explod();
            }
            //从左方向旋转至目标方向
            this.transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
            //按帧朝方向运动
            this.transform.position += speed * Time.deltaTime * dir.normalized;
        }
    }

    /// <summary>
    /// 导弹发射
    /// </summary>
    public void Launch()
    {
        running = true;
    }

    /// <summary>
    /// 导弹爆炸
    /// </summary>
    private void Explod()
    {
        Destroy(this.gameObject);
        Instantiate(fxExpold, this.transform.position, Quaternion.identity);

        //再此判断是否击中玩家
        if (target != null && (target.position - this.transform.position).magnitude < 0.1)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(power);
        }
    }

}
