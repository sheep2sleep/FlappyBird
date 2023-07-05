using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    //无敌时间
    public float invincibleTime = 3f;
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
    /// 玩家重生
    /// </summary>
    public void Rebirth()
    {
        //使用协程来等待一段时间执行
        StartCoroutine(DoRebirth());
    }

    /// <summary>
    /// 玩家重生协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator DoRebirth()
    {
        //等待1秒重生
        yield return new WaitForSeconds(1f);
        timer = 0;
        this.Init();
        this.Fly();
    }

    /// <summary>
    /// 玩家是否无敌
    /// </summary>
    public bool IsInvincible
    {
        get { return timer < this.invincibleTime; }
    }

    /// <summary>
    /// 碰撞到触发器
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        //状态为死亡时不触发（用于复活时的无敌时间）
        if (this.death) return;
        if (this.IsInvincible) return;

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
