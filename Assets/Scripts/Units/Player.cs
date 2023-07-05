using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    private float timer = 0;

    /// <summary>
    /// 继承的虚函数，执行每帧更新逻辑
    /// </summary>
    public override void OnUpdate()
    {
        if (this.death) return;

        timer += Time.deltaTime;

        //键盘控制小鸟移动
        Vector2 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        this.transform.position = pos;

        //ctrl和左键控制小鸟开火
        if (Input.GetButton("Fire1"))
        {
            this.Fire();
        }
    }

    /// <summary>
    /// 碰撞到触发器
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        Element bullet = other.gameObject.GetComponent<Element>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (bullet == null && enemy == null)
        {
            return;
        }
        Debug.LogFormat("{0}触发了{1} {2}", gameObject.name, other.gameObject.name, Time.time);

        //触发到敌人
        if(enemy != null)
        {
            Die();
        }
        //触发到敌方子弹
        if(bullet != null && bullet.side == SIDE.ENEMY)
        {
            Damage(bullet.power); 
        }
    }
}
