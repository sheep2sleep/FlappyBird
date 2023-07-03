using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float speed;
    public float fireRate;
    public float destroyTime = 10f;
    public GameObject bulletTemplate;

    private Vector3 initPosion;
    private bool death = false;

    //定义无参无返回值的委托
    public delegate void DeathNotify();
    //声明一个委托
    public event DeathNotify OnDeath;

    //使用事件传递积分消息
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Fly();
        initPosion = transform.position;
        Destroy(this.gameObject, destroyTime);
    }

    float fireTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (death) return;

        fireTimer += Time.deltaTime;

        //敌人自己移动
        transform.position += new Vector3(-Time.deltaTime * speed, 0);
        Fire();
    }

    /// <summary>
    /// 敌人开火
    /// </summary>
    public void Fire()
    {
        if (fireTimer > 1 / fireRate)
        {
            GameObject bullt = Instantiate(bulletTemplate);
            bullt.transform.position = transform.position;

            fireTimer = 0f;
        }
    }

    /// <summary>
    /// 切换状态为待机
    /// </summary>
    public void Idle()
    {
        rigidbodyBird.simulated = false;
        ani.SetTrigger("Idle");
    }

    /// <summary>
    /// 切换状态为飞行
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
    }

    /// <summary>
    /// 切换状态为死亡
    /// </summary>
    public void Die()
    {
        if (death == false)
        {
            death = true;
            ani.SetTrigger("Die");
            if (OnDeath != null)//当委托已绑定时
            {
                OnDeath();//触发委托
            }
            Destroy(this.gameObject,0.2f);
        }
    }

    /// <summary>
    /// 碰撞到地面碰撞器
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Die();
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
            Die();
        }
    }

    /// <summary>
    /// 重新开始游戏时初始化小鸟状态
    /// </summary>
    public void Restart()
    {
        transform.position = initPosion;
        death = false;
        Idle();
    }
}
