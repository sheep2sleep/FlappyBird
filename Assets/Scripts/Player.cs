using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{    
    //声明一个委托--死亡
    public event DeathNotify OnDeath;

    //使用事件传递积分消息
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        initPosion = transform.position;
    }


    
    // Update is called once per frame
    void Update()
    {      
        //键盘控制小鸟移动
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = pos;

        //ctrl和左键控制小鸟开火
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    /// <summary>
    /// 切换状态为死亡
    /// </summary>
    public void Die()
    {
        if(death == false)
        {
            death = true;
            if (OnDeath != null)//当委托已绑定时
            {
                OnDeath();//触发委托
            }
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
            HP = 0;
            Die();
        }

        //触发到敌方子弹
        if(bullet != null && bullet.side == SIDE.ENEMY)
        {
            HP = HP - bullet.power;
            if (HP <= 0)
            {
                Die();
            }   
        }
    }

    
 
}
