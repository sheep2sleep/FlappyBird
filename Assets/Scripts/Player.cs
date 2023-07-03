using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidbodyBird;
    public Animator ani;
    public float speed;
    public float fireRate;
    public GameObject bulletTemplate;

    private Vector3 initPosion;
    private bool isFlying = false;
    private bool death = false;

    public float HP = 100;



    //定义无参无返回值的委托
    public delegate void DeathNotify();
    //声明一个委托
    public event DeathNotify OnDeath;

    //使用事件传递积分消息
    public UnityAction<int> OnScore;

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        initPosion = transform.position;
    }


    float fireTimer = 0;
    // Update is called once per frame
    void Update()
    {
        if (death) return;
        if (!isFlying) return;

        fireTimer += Time.deltaTime;

        //键盘控制小鸟移动
        Vector2 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = pos;

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    /// <summary>
    /// 小鸟开火
    /// </summary>
    public void Fire()
    {
        if(fireTimer > 1 / fireRate)
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
        this.isFlying = false;
    }
    
    /// <summary>
    /// 切换状态为飞行
    /// </summary>
    public void Fly()
    {
        rigidbodyBird.simulated = true;
        ani.SetTrigger("Fly");
        this.isFlying = true;
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
        Debug.LogFormat("{0}触发了{1} {2}", gameObject.name, other.gameObject.name, Time.time);
        if(bullet.side == SIDE.ENEMY)
        {
            HP = HP - bullet.power;
            if (HP <= 0)
            {
                Die();
            }   
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
