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

    /// <summary>
    /// 导弹每帧更新
    /// </summary>
    public override void OnUpdate()
    {
        if (!running)
            return;

        if (target != null)
        {
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

        if (target != null)
        {
            Player p = target.GetComponent<Player>();
            p.Damage(power);
        }
    }

}
