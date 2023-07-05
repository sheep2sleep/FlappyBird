using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    public ENEMY_TYPE enemyType;
    public Vector2 range;
    public float destroyTime = 10f;
    

    private float initY = 0;

    /// <summary>
    /// 继承的虚函数，执行初始化逻辑
    /// </summary>
    public override void OnStart()
    {
        Destroy(this.gameObject, destroyTime);
        initY = Random.Range(range.x, range.y);
        transform.localPosition = new Vector3(0, initY, 0);
        Fly();
    }

    /// <summary>
    /// 继承的虚函数，执行每帧更新逻辑
    /// </summary>
    public override void OnUpdate()
    {
        //为摇摆小鸟添加摇摆
        float y = 0;
        if (enemyType == ENEMY_TYPE.SWING_ENEMY)
        {
            y = Mathf.Sin(Time.timeSinceLevelLoad) * 3f;
        }

        //敌人自己移动
        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, initY + y);
        //敌人自动开火
        Fire();
    }

    /// <summary>
    /// 碰撞到触发器
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        Element bullet = other.gameObject.GetComponent<Element>();
        if (bullet == null)
        {
            return;
        }
        Debug.LogFormat("{0}触发了{1} {2}", this.gameObject.name, other.gameObject.name, Time.time);
        if (bullet.side == SIDE.PLAYER)
        {
            Damage(bullet.power);
        }
    }
}
